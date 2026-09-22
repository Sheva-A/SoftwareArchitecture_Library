namespace Library.Application.BookCatalog.DTOs;

// DTO для запиту на створення книги (вхідні дані від клієнта).
public class CreateBookDto
{
    public string Title { get; set; } = string.Empty;
    public string Isbn { get; set; } = string.Empty;
    public Guid AuthorId { get; set; }
    public int PublicationYear { get; set; }
}
