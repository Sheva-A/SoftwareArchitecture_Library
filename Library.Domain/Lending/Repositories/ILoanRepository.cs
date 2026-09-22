using Library.Domain.Lending.Entities;

namespace Library.Domain.Lending.Repositories;

// Інтерфейс репозиторію позик — оголошується в доменному шарі.
public interface ILoanRepository
{
    Task<Loan?> GetByIdAsync(Guid id);
    Task<IEnumerable<Loan>> GetAllAsync();
    Task<IEnumerable<Loan>> GetByMemberIdAsync(Guid memberId);
    Task<IEnumerable<Loan>> GetActiveLoansAsync();
    Task AddAsync(Loan loan);
    Task UpdateAsync(Loan loan);
}
