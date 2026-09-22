using Library.Application.Common;
using Library.Domain.Common;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Infrastructure.Events;

// Реалізація диспетчера подій у пам'яті.
// Отримує обробники через IServiceProvider — це забезпечує слабке зв'язування
// між виробником події та її обробниками.
public class InMemoryEventDispatcher : IEventDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public InMemoryEventDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task DispatchAsync<TEvent>(TEvent domainEvent) where TEvent : IDomainEvent
    {
        // Отримати всі зареєстровані обробники для даного типу події
        var handlers = _serviceProvider.GetServices<IEventHandler<TEvent>>();
        foreach (var handler in handlers)
        {
            await handler.HandleAsync(domainEvent);
        }
    }
}
