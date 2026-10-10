namespace Library.Domain.Lending;

// Інтерфейс сховища видач, реалізація в Infrastructure
public interface ILoanRepository
{
    void Add(Loan loan);
    Loan? GetById(Guid id);
    IReadOnlyList<Loan> GetAll();
}
