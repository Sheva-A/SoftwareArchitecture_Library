using Library.Application.Common;
using Library.Domain.BookCatalog.Events;

namespace Library.Application.BookCatalog.EventHandlers;

// Обробник події додавання книги до каталогу
public class BookCreatedEventHandler : IEventHandler<BookCreatedEvent>
{
    public Task HandleAsync(BookCreatedEvent domainEvent)
    {
        // У реальному проєкті тут може бути: індексація для пошуку, сповіщення підписників тощо
        Console.WriteLine(
            $"[Event] Книгу «{domainEvent.Title}» (Id: {domainEvent.BookId}) додано до каталогу о {domainEvent.OccurredAt:u}");
        return Task.CompletedTask;
    }
}
