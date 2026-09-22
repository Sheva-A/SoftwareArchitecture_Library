using Library.Domain.Common;

namespace Library.Domain.Lending.Events;

// Доменна подія: книгу видано читачу
public record BookBorrowedEvent(Guid BookId, Guid MemberId, DateTime OccurredAt) : IDomainEvent;
