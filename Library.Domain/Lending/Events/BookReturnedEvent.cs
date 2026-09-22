using Library.Domain.Common;

namespace Library.Domain.Lending.Events;

// Доменна подія: книгу повернено читачем
public record BookReturnedEvent(Guid BookId, Guid MemberId, DateTime OccurredAt) : IDomainEvent;
