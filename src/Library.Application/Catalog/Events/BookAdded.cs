namespace Library.Application.Catalog.Events;

// Подія: до каталогу додали нову книгу
public record BookAdded(Guid BookId, string Title, string Author);
