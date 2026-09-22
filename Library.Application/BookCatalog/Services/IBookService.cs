using Library.Application.BookCatalog.DTOs;

namespace Library.Application.BookCatalog.Services;

// Інтерфейс сервісу книг — оголошується в Application.
// Контролери залежать від цього інтерфейсу, а не від конкретної реалізації.
public interface IBookService
{
    Task<IEnumerable<BookDto>> GetAllAsync();
    Task<IEnumerable<BookDto>> GetAvailableAsync();
    Task<BookDto?> GetByIdAsync(Guid id);
    Task<BookDto> CreateAsync(CreateBookDto dto);
}
