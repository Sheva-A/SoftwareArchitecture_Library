using Library.Application.Contracts;
using Library.Application.Lending.DTOs;
using Library.Domain.Lending.Entities;
using Library.Domain.Lending.Repositories;
using Library.Domain.Lending.ValueObjects;

namespace Library.Application.Lending.Services;

// Реалізація сервісу позик.
// Демонструє взаємодію між bounded contexts через контракт IBookAvailabilityService.
public class LoanService : ILoanService
{
    private readonly ILoanRepository _loanRepository;
    private readonly IMemberRepository _memberRepository;

    // Зв'язок з bounded context BookCatalog — лише через інтерфейс-контракт
    private readonly IBookAvailabilityService _bookAvailabilityService;

    public LoanService(
        ILoanRepository loanRepository,
        IMemberRepository memberRepository,
        IBookAvailabilityService bookAvailabilityService)
    {
        _loanRepository = loanRepository;
        _memberRepository = memberRepository;
        _bookAvailabilityService = bookAvailabilityService;
    }

    public async Task<IEnumerable<LoanDto>> GetAllAsync()
    {
        var loans = await _loanRepository.GetAllAsync();
        return await MapToDtosAsync(loans);
    }

    public async Task<IEnumerable<LoanDto>> GetActiveAsync()
    {
        var loans = await _loanRepository.GetActiveLoansAsync();
        return await MapToDtosAsync(loans);
    }

    public async Task<LoanDto?> GetByIdAsync(Guid id)
    {
        var loan = await _loanRepository.GetByIdAsync(id);
        if (loan is null) return null;

        var member = await _memberRepository.GetByIdAsync(loan.MemberId);
        return MapToDto(loan, member?.FullName ?? "Невідомо");
    }

    public async Task<LoanDto> BorrowBookAsync(CreateLoanDto dto)
    {
        // Перевірити читача
        var member = await _memberRepository.GetByIdAsync(dto.MemberId)
            ?? throw new ArgumentException("Читача не знайдено.");

        // Перевірити доступність книги через cross-context контракт
        var isAvailable = await _bookAvailabilityService.IsBookAvailableAsync(dto.BookId);
        if (!isAvailable)
            throw new InvalidOperationException("Книга недоступна для видачі.");

        // Бізнес-правило: читач не може взяти нову книгу, маючи прострочену позику
        var memberLoans = await _loanRepository.GetByMemberIdAsync(dto.MemberId);
        var hasOverdueLoans = memberLoans.Any(l => !l.IsReturned && l.Period.IsOverdue);
        if (hasOverdueLoans)
            throw new InvalidOperationException("Читач має прострочені позики і не може отримати нову книгу.");

        // Створити позику через доменний об'єкт
        var period = LoanPeriod.Create(DateTime.UtcNow, durationDays: 14);
        var loan = new Loan(dto.BookId, dto.MemberId, period);

        await _loanRepository.AddAsync(loan);

        // Повідомити BookCatalog BC про видачу (через контракт)
        await _bookAvailabilityService.MarkBookAsCheckedOutAsync(dto.BookId);

        return MapToDto(loan, member.FullName);
    }

    public async Task<LoanDto> ReturnBookAsync(Guid loanId)
    {
        var loan = await _loanRepository.GetByIdAsync(loanId)
            ?? throw new ArgumentException("Позику не знайдено.");

        // Виклик доменного методу — бізнес-правило повернення у сутності Loan
        loan.Return();
        await _loanRepository.UpdateAsync(loan);

        // Повідомити BookCatalog BC, що книга повернута
        await _bookAvailabilityService.MarkBookAsReturnedAsync(loan.BookId);

        var member = await _memberRepository.GetByIdAsync(loan.MemberId);
        return MapToDto(loan, member?.FullName ?? "Невідомо");
    }

    private async Task<IEnumerable<LoanDto>> MapToDtosAsync(IEnumerable<Loan> loans)
    {
        var members = (await _memberRepository.GetAllAsync())
            .ToDictionary(m => m.Id, m => m.FullName);

        return loans.Select(l => MapToDto(l,
            members.TryGetValue(l.MemberId, out var name) ? name : "Невідомо"));
    }

    private static LoanDto MapToDto(Loan loan, string memberName) => new()
    {
        Id = loan.Id,
        BookId = loan.BookId,
        MemberId = loan.MemberId,
        MemberName = memberName,
        BorrowedAt = loan.Period.BorrowedAt,
        DueDate = loan.Period.DueDate,
        ReturnedAt = loan.ReturnedAt,
        IsReturned = loan.IsReturned,
        IsOverdue = !loan.IsReturned && loan.Period.IsOverdue
    };
}
