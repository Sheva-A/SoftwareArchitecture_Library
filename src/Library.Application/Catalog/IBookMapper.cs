using Library.Domain.Catalog;

namespace Library.Application.Catalog;

// Мапінг доменної сутності Book у DTO
public interface IBookMapper
{
    AddBookResultDto ToAddResultDto(Book book);
    BookDto ToDto(Book book);
}
