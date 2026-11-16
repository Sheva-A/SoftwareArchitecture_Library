namespace Library.Application.Events;

// Спільний тип усіх обробників, щоб передавати їх через конструктор
public interface IEventHandler
{
}

// Обробник однієї події, окремий сценарій реакції системи
public interface IEventHandler<in TEvent> : IEventHandler
{
    void Handle(TEvent evt);
}
