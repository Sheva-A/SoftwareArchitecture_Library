using Library.Domain.Lending;

namespace Library.Infrastructure.Repositories;

// Зберігання видач у пам'яті програми
public class InMemoryLoanRepository : ILoanRepository
{
    private readonly List<Loan> _loans = new();

    public void Add(Loan loan)
    {
        _loans.Add(loan);
    }

    public Loan? GetById(Guid id)
    {
        return _loans.FirstOrDefault(l => l.Id == id);
    }

    public IReadOnlyList<Loan> GetAll()
    {
        return _loans;
    }
}
