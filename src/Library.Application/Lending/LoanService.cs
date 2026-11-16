using Library.Application.Catalog;
using Library.Application.Common;
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
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventBus _events;
    private readonly IClock _clock;
    private readonly ILoanMapper _mapper;
    private readonly IBookListCache _bookListCache;

    public LoanService(ILoanRepository loans, IBookCatalog catalog, IUnitOfWork unitOfWork,
        IEventBus events, IClock clock, ILoanMapper mapper, IBookListCache bookListCache)
    {
        _loans = loans;
        _catalog = catalog;
        _unitOfWork = unitOfWork;
        _events = events;
        _clock = clock;
        _mapper = mapper;
        _bookListCache = bookListCache;
    }

    public BorrowBookResultDto BorrowBook(BorrowBookDto dto)
    {
        var loan = new Loan(dto.BookId, dto.ReaderName, _clock.Now);

        // Каталог перевіряє, чи книга доступна, і позначає її виданою
        _catalog.BorrowBook(dto.BookId);

        _loans.Add(loan);

        // Зміну книги та нову видачу зберігаємо разом
        _unitOfWork.SaveChanges();

        // Наявність книги змінилась, тому список книг у кеші застарів
        _bookListCache.Remove();

        // Публікуємо подію, не знаючи, хто її обробить
        _events.Publish(new BookBorrowed(loan.Id, loan.BookId, loan.ReaderName, loan.DueDate));

        // Назовні повертаємо DTO, а не доменну сутність
        return _mapper.ToBorrowResultDto(loan);
    }

    public ReturnBookResultDto ReturnBook(Guid loanId)
    {
        var loan = _loans.GetById(loanId) ?? throw new KeyNotFoundException("Видачу не знайдено");

        loan.Return(_clock.Now);

        // Повідомляємо каталог, що книга знову доступна
        _catalog.ReturnBook(loan.BookId);

        // Зміни видачі та книги зберігаємо разом
        _unitOfWork.SaveChanges();

        // Наявність книги змінилась, тому список книг у кеші застарів
        _bookListCache.Remove();

        return _mapper.ToReturnResultDto(loan);
    }

    public IReadOnlyList<LoanDto> GetAll()
    {
        return _loans.GetAll().Select(_mapper.ToDto).ToList();
    }
}
