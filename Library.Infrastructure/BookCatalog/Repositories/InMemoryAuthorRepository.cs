using Library.Domain.BookCatalog.Entities;
using Library.Domain.BookCatalog.Repositories;

namespace Library.Infrastructure.BookCatalog.Repositories;

// In-memory реалізація репозиторію авторів
public class InMemoryAuthorRepository : IAuthorRepository
{
    private readonly List<Author> _authors = new();

    public Task<Author?> GetByIdAsync(Guid id)
        => Task.FromResult(_authors.FirstOrDefault(a => a.Id == id));

    public Task<IEnumerable<Author>> GetAllAsync()
        => Task.FromResult<IEnumerable<Author>>(_authors);

    public Task AddAsync(Author author)
    {
        _authors.Add(author);
        return Task.CompletedTask;
    }
}
