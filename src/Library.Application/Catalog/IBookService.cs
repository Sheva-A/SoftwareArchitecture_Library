using Library.Domain.Catalog;

namespace Library.Application.Catalog;

// Абстракція сервісу каталогу, від неї залежить контролер
public interface IBookService
{
    Book AddBook(string title, string author, string isbn);
    IReadOnlyList<Book> GetAll();
}
