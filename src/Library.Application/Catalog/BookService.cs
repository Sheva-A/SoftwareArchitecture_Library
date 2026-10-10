using Library.Domain.Catalog;

namespace Library.Application.Catalog;

// Сценарії роботи з каталогом книг
public class BookService : IBookCatalog
{
    private readonly IBookRepository _books;

    public BookService(IBookRepository books)
    {
        _books = books;
    }

    public Book AddBook(string title, string author, string isbn)
    {
        // Перевірки виконує сама доменна модель
        var book = new Book(title, author, new Isbn(isbn));
        _books.Add(book);
        return book;
    }

    public IReadOnlyList<Book> GetAll()
    {
        return _books.GetAll();
    }

    public void BorrowBook(Guid bookId)
    {
        GetBook(bookId).MarkAsBorrowed();
    }

    public void ReturnBook(Guid bookId)
    {
        GetBook(bookId).MarkAsReturned();
    }

    private Book GetBook(Guid bookId)
    {
        return _books.GetById(bookId) ?? throw new KeyNotFoundException("Книгу не знайдено");
    }
}
