using Library.Application.Catalog;
using Library.Application.Events;
using Library.Application.Lending.Events;
using Library.Domain.Common;
using Library.Domain.Lending;

namespace Library.Application.Lending;

// Сценарії видачі та повернення книг
public class LoanService : ILoanService
{
    private readonly ILoanRepository _loans;
    private readonly IBookCatalog _catalog;
    private readonly IEventBus _events;
    private readonly IClock _clock;

    public LoanService(ILoanRepository loans, IBookCatalog catalog, IEventBus events, IClock clock)
    {
        _loans = loans;
        _catalog = catalog;
        _events = events;
        _clock = clock;
    }

    public Loan BorrowBook(Guid bookId, string readerName)
    {
        var loan = new Loan(bookId, readerName, _clock.Now);

        // Каталог перевіряє, чи книга доступна, і позначає її виданою
        _catalog.BorrowBook(bookId);

        _loans.Add(loan);

        // Публікуємо подію, не знаючи, хто її обробить
        _events.Publish(new BookBorrowed(loan.Id, loan.BookId, loan.ReaderName, loan.DueDate));

        return loan;
    }

    public Loan ReturnBook(Guid loanId)
    {
        var loan = _loans.GetById(loanId) ?? throw new KeyNotFoundException("Видачу не знайдено");

        loan.Return(_clock.Now);

        // Повідомляємо каталог, що книга знову доступна
        _catalog.ReturnBook(loan.BookId);

        return loan;
    }

    public IReadOnlyList<Loan> GetAll()
    {
        return _loans.GetAll();
    }
}
