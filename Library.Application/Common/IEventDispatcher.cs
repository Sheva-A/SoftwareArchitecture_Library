using Library.Domain.Common;

namespace Library.Application.Common;

// Інтерфейс диспетчера подій — відв'язує виробника від обробників
public interface IEventDispatcher
{
    Task DispatchAsync<TEvent>(TEvent domainEvent) where TEvent : IDomainEvent;
}
