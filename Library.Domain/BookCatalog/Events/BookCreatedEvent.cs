using Library.Domain.Common;

namespace Library.Domain.BookCatalog.Events;

// Доменна подія: нову книгу додано до каталогу
public record BookCreatedEvent(Guid BookId, string Title, DateTime OccurredAt) : IDomainEvent;
