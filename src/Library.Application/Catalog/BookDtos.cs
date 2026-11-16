namespace Library.Application.Catalog;

// Дані для додавання книги
public record AddBookDto(string Title, string Author, string Isbn);

// Результат додавання книги
public record AddBookResultDto(Guid Id, string Title, string Author, string Isbn);

// Книга у списку каталогу
public record BookDto(Guid Id, string Title, string Author, string Isbn, bool IsAvailable);
