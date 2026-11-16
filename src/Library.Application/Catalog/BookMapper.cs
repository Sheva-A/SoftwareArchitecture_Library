using Library.Domain.Catalog;

namespace Library.Application.Catalog;

// Перетворює книгу в DTO вручну, поле за полем
public class BookMapper : IBookMapper
{
    public AddBookResultDto ToAddResultDto(Book book)
    {
        return new AddBookResultDto(book.Id, book.Title, book.Author, book.Isbn.Value);
    }

    public BookDto ToDto(Book book)
    {
        return new BookDto(book.Id, book.Title, book.Author, book.Isbn.Value, book.IsAvailable);
    }
}
