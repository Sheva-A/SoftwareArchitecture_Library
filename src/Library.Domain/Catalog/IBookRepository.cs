namespace Library.Domain.Catalog;

// Інтерфейс сховища книг, реалізація в Infrastructure
public interface IBookRepository
{
    void Add(Book book);
    Book? GetById(Guid id);
    IReadOnlyList<Book> GetAll();
}
