using Library.Domain.Catalog;

namespace Library.Application.Catalog;

// Інтерфейс репозиторію книг, реалізація в Infrastructure
public interface IBookRepository
{
    void Add(Book book);
    Book? GetById(Guid id);
    IReadOnlyList<Book> GetAll();
}
