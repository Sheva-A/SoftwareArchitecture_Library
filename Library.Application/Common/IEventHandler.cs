using Library.Domain.Common;

namespace Library.Application.Common;

// Інтерфейс обробника доменної події
public interface IEventHandler<TEvent> where TEvent : IDomainEvent
{
    Task HandleAsync(TEvent domainEvent);
}
