using Library.Domain.BookCatalog.Entities;
using Library.Domain.BookCatalog.Repositories;

namespace Library.Infrastructure.BookCatalog.Repositories;

// In-memory реалізація репозиторію книг.
// У реальному проєкті замінюється на EF Core або інший ORM — завдяки інтерфейсу IBookRepository.
public class InMemoryBookRepository : IBookRepository
{
    // Спільне сховище для всіх запитів у межах одного процесу
    private readonly List<Book> _books = new();

    public Task<Book?> GetByIdAsync(Guid id)
    {
        var book = _books.FirstOrDefault(b => b.Id == id);
        return Task.FromResult(book);
    }

    public Task<IEnumerable<Book>> GetAllAsync()
        => Task.FromResult<IEnumerable<Book>>(_books);

    public Task<IEnumerable<Book>> GetAvailableAsync()
        => Task.FromResult<IEnumerable<Book>>(_books.Where(b => b.IsAvailable));

    public Task AddAsync(Book book)
    {
        _books.Add(book);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Book book)
    {
        // Для in-memory сховища оновлення не потрібне — об'єкт вже оновлено в пам'яті
        return Task.CompletedTask;
    }
}
