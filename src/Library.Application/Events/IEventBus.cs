namespace Library.Application.Events;

// Публікація події: ініціатор (producer) не знає, хто її обробить
public interface IEventBus
{
    void Publish<TEvent>(TEvent evt);
}
