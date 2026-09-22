using Library.Domain.Lending.Entities;
using Library.Domain.Lending.Repositories;

namespace Library.Infrastructure.Lending.Repositories;

// In-memory реалізація репозиторію позик
public class InMemoryLoanRepository : ILoanRepository
{
    private readonly List<Loan> _loans = new();

    public Task<Loan?> GetByIdAsync(Guid id)
        => Task.FromResult(_loans.FirstOrDefault(l => l.Id == id));

    public Task<IEnumerable<Loan>> GetAllAsync()
        => Task.FromResult<IEnumerable<Loan>>(_loans);

    public Task<IEnumerable<Loan>> GetByMemberIdAsync(Guid memberId)
        => Task.FromResult<IEnumerable<Loan>>(_loans.Where(l => l.MemberId == memberId));

    public Task<IEnumerable<Loan>> GetActiveLoansAsync()
        => Task.FromResult<IEnumerable<Loan>>(_loans.Where(l => !l.IsReturned));

    public Task AddAsync(Loan loan)
    {
        _loans.Add(loan);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Loan loan)
    {
        // Для in-memory сховища оновлення не потрібне
        return Task.CompletedTask;
    }
}
