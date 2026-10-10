using Library.Domain.Catalog;

namespace Library.Infrastructure.Repositories;

// Зберігання книг у пам'яті програми
public class InMemoryBookRepository : IBookRepository
{
    private readonly List<Book> _books = new();

    public void Add(Book book)
    {
        _books.Add(book);
    }

    public Book? GetById(Guid id)
    {
        return _books.FirstOrDefault(b => b.Id == id);
    }

    public IReadOnlyList<Book> GetAll()
    {
        return _books;
    }
}
