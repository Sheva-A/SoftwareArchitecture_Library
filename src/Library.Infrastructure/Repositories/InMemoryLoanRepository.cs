using Library.Application.Lending;
using Library.Domain.Lending;
using Library.Infrastructure.Persistence;

namespace Library.Infrastructure.Repositories;

// Репозиторій видач: дані в пам'яті, зміни зберігаються через Unit of Work
public class InMemoryLoanRepository : ILoanRepository
{
    private readonly InMemoryStore _store;
    private readonly InMemoryUnitOfWork _unitOfWork;

    public InMemoryLoanRepository(InMemoryStore store, InMemoryUnitOfWork unitOfWork)
    {
        _store = store;
        _unitOfWork = unitOfWork;
    }

    public void Add(Loan loan)
    {
        // Нова видача потрапить у сховище лише після SaveChanges
        _unitOfWork.Loans[loan.Id] = loan;
    }

    public Loan? GetById(Guid id)
    {
        // Якщо видачу вже отримували в цьому запиті, повертаємо той самий об'єкт
        if (_unitOfWork.Loans.TryGetValue(id, out var loan))
            return loan;

        if (!_store.Loans.TryGetValue(id, out var saved))
            return null;

        // Повертаємо копію, щоб зміни не потрапили у сховище до SaveChanges
        loan = EntityCopy.Of(saved);
        _unitOfWork.Loans[id] = loan;
        return loan;
    }

    public IReadOnlyList<Loan> GetAll()
    {
        return _store.Loans.Values.ToList();
    }
}
