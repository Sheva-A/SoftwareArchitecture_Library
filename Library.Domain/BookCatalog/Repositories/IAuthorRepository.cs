using Library.Domain.BookCatalog.Entities;

namespace Library.Domain.BookCatalog.Repositories;

// Інтерфейс репозиторію авторів — оголошується в доменному шарі.
public interface IAuthorRepository
{
    Task<Author?> GetByIdAsync(Guid id);
    Task<IEnumerable<Author>> GetAllAsync();
    Task AddAsync(Author author);
}
