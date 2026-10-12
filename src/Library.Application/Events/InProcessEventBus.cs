using Microsoft.Extensions.DependencyInjection;

namespace Library.Application.Events;

// Передає подію всім її обробникам усередині застосунку
public class InProcessEventBus : IEventBus
{
    private readonly IServiceProvider _services;

    public InProcessEventBus(IServiceProvider services)
    {
        _services = services;
    }

    public void Publish<TEvent>(TEvent evt)
    {
        // Отримуємо всі обробники цієї події і викликаємо їх
        var handlers = _services.GetServices<IEventHandler<TEvent>>();
        foreach (var handler in handlers)
            handler.Handle(evt);
    }
}
