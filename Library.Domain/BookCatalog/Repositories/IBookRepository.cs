using Library.Domain.BookCatalog.Entities;

namespace Library.Domain.BookCatalog.Repositories;

// Інтерфейс репозиторію книг — оголошується в доменному шарі.
// Реалізація знаходиться в Infrastructure, що забезпечує інверсію залежностей.
public interface IBookRepository
{
    Task<Book?> GetByIdAsync(Guid id);
    Task<IEnumerable<Book>> GetAllAsync();
    Task<IEnumerable<Book>> GetAvailableAsync();
    Task AddAsync(Book book);
    Task UpdateAsync(Book book);
}
