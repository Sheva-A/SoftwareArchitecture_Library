namespace Library.Application.Catalog;

// Кеш списку книг, реалізація в Infrastructure
public interface IBookListCache
{
    // Повертає null, якщо списку в кеші немає
    IReadOnlyList<BookDto>? Get();
    void Set(IReadOnlyList<BookDto> books);
    void Remove();
}
