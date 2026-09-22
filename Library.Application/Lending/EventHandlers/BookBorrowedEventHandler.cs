using Library.Application.Common;
using Library.Domain.Lending.Events;

namespace Library.Application.Lending.EventHandlers;

// Обробник події видачі книги
public class BookBorrowedEventHandler : IEventHandler<BookBorrowedEvent>
{
    public Task HandleAsync(BookBorrowedEvent domainEvent)
    {
        // У реальному проєкті тут може бути: запис до журналу аудиту, надсилання сповіщення тощо
        Console.WriteLine(
            $"[Event] Книгу {domainEvent.BookId} видано читачу {domainEvent.MemberId} о {domainEvent.OccurredAt:u}");
        return Task.CompletedTask;
    }
}
