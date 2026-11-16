namespace Library.Application.Events;

// Передає подію всім її обробникам усередині застосунку
public class InProcessEventBus : IEventBus
{
    private readonly IEnumerable<IEventHandler> _handlers;

    // Обробники передаються через конструктор, клас не звертається до DI-контейнера
    public InProcessEventBus(IEnumerable<IEventHandler> handlers)
    {
        _handlers = handlers;
    }

    public void Publish<TEvent>(TEvent evt)
    {
        // Вибираємо обробники цієї події і викликаємо їх
        foreach (var handler in _handlers.OfType<IEventHandler<TEvent>>())
            handler.Handle(evt);
    }
}
