namespace Library.Application.BookCatalog.DTOs;

// DTO для відображення автора
public class AuthorDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
}
