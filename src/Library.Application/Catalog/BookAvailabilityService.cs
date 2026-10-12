using Library.Domain.Catalog;

namespace Library.Application.Catalog;

// Зміна наявності книги під час видачі та повернення
public class BookAvailabilityService : IBookCatalog
{
    private readonly IBookRepository _books;

    public BookAvailabilityService(IBookRepository books)
    {
        _books = books;
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
