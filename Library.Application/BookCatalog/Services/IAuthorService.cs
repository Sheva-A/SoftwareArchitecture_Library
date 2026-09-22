using Library.Application.BookCatalog.DTOs;

namespace Library.Application.BookCatalog.Services;

// Інтерфейс сервісу авторів
public interface IAuthorService
{
    Task<IEnumerable<AuthorDto>> GetAllAsync();
    Task<AuthorDto?> GetByIdAsync(Guid id);
    Task<AuthorDto> CreateAsync(CreateAuthorDto dto);
}
