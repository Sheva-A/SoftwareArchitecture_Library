namespace Library.Application.Catalog;

// Абстракція сервісу каталогу, приймає і повертає лише DTO
public interface IBookService
{
    AddBookResultDto AddBook(AddBookDto dto);
    IReadOnlyList<BookDto> GetAll();
}
