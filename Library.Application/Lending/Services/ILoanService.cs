using Library.Application.Lending.DTOs;

namespace Library.Application.Lending.Services;

// Інтерфейс сервісу позик
public interface ILoanService
{
    Task<IEnumerable<LoanDto>> GetAllAsync();
    Task<IEnumerable<LoanDto>> GetActiveAsync();
    Task<LoanDto?> GetByIdAsync(Guid id);
    Task<LoanDto> BorrowBookAsync(CreateLoanDto dto);
    Task<LoanDto> ReturnBookAsync(Guid loanId);
}
