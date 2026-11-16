using Library.Application.Catalog.Events;
using Library.Application.Common;
using Library.Application.Events;
using Library.Domain.Catalog;

namespace Library.Application.Catalog;

// Сценарії роботи з каталогом книг: додавання та перегляд
public class BookService : IBookService
{
    private readonly IBookRepository _books;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventBus _events;

    public BookService(IBookRepository books, IUnitOfWork unitOfWork, IEventBus events)
    {
        _books = books;
        _unitOfWork = unitOfWork;
        _events = events;
    }

    public Book AddBook(string title, string author, string isbn)
    {
        // Перевірки виконує сама доменна модель
        var book = new Book(title, author, new Isbn(isbn));
        _books.Add(book);

        // Зберігаємо зміни через Unit of Work
        _unitOfWork.SaveChanges();

        // Публікуємо подію, не знаючи, хто її обробить
        _events.Publish(new BookAdded(book.Id, book.Title, book.Author));

        return book;
    }

    public IReadOnlyList<Book> GetAll()
    {
        return _books.GetAll();
    }
}