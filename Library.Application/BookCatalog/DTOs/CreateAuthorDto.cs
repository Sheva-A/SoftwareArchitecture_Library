namespace Library.Application.BookCatalog.DTOs;

// DTO для запиту на створення автора
public class CreateAuthorDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}
