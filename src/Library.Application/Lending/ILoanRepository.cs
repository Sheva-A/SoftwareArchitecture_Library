using Library.Domain.Lending;

namespace Library.Application.Lending;

// Інтерфейс репозиторію видач, реалізація в Infrastructure
public interface ILoanRepository
{
    void Add(Loan loan);
    Loan? GetById(Guid id);
    IReadOnlyList<Loan> GetAll();
}
