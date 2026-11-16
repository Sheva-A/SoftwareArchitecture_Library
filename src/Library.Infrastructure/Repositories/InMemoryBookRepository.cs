using Library.Application.Catalog;
using Library.Domain.Catalog;
using Library.Infrastructure.Persistence;

namespace Library.Infrastructure.Repositories;

// Репозиторій книг: дані в пам'яті, зміни зберігаються через Unit of Work
public class InMemoryBookRepository : IBookRepository
{
    private readonly InMemoryStore _store;
    private readonly InMemoryUnitOfWork _unitOfWork;

    public InMemoryBookRepository(InMemoryStore store, InMemoryUnitOfWork unitOfWork)
    {
        _store = store;
        _unitOfWork = unitOfWork;
    }

    public void Add(Book book)
    {
        // Нова книга потрапить у сховище лише після SaveChanges
        _unitOfWork.Books[book.Id] = book;
    }

    public Book? GetById(Guid id)
    {
        // Якщо книгу вже отримували в цьому запиті, повертаємо той самий об'єкт
        if (_unitOfWork.Books.TryGetValue(id, out var book))
            return book;

        if (!_store.Books.TryGetValue(id, out var saved))
            return null;

        // Повертаємо копію, щоб зміни не потрапили у сховище до SaveChanges
        book = EntityCopy.Of(saved);
        _unitOfWork.Books[id] = book;
        return book;
    }

    public IReadOnlyList<Book> GetAll()
    {
        return _store.Books.Values.ToList();
    }
}
