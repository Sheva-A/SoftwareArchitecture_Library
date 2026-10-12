namespace Library.Application.Events;

// Обробник однієї події, окремий сценарій реакції системи
public interface IEventHandler<in TEvent>
{
    void Handle(TEvent evt);
}
