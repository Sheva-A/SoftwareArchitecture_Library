namespace Library.Application.BookCatalog.DTOs;

// DTO для передачі даних книги з Application до WebApi.
// Не містить доменної логіки — лише дані для відповіді.
public class BookDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Isbn { get; set; } = string.Empty;
    public string AuthorName { get; set; } = string.Empty;
    public int PublicationYear { get; set; }
    public bool IsAvailable { get; set; }
}
