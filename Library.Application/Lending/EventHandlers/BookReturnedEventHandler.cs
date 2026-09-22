using Library.Application.Common;
using Library.Domain.Lending.Events;

namespace Library.Application.Lending.EventHandlers;

// Обробник події повернення книги
public class BookReturnedEventHandler : IEventHandler<BookReturnedEvent>
{
    public Task HandleAsync(BookReturnedEvent domainEvent)
    {
        // У реальному проєкті тут може бути: оновлення статистики, перевірка штрафів тощо
        Console.WriteLine(
            $"[Event] Книгу {domainEvent.BookId} повернено читачем {domainEvent.MemberId} о {domainEvent.OccurredAt:u}");
        return Task.CompletedTask;
    }
}
