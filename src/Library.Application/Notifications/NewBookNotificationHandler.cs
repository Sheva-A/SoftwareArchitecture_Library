using Library.Application.Catalog.Events;
using Library.Application.Events;
using Microsoft.Extensions.Logging;

namespace Library.Application.Notifications;

// Повідомляє читачів про нову книгу в каталозі
public class NewBookNotificationHandler : IEventHandler<BookAdded>
{
    private readonly ILogger<NewBookNotificationHandler> _logger;

    public NewBookNotificationHandler(ILogger<NewBookNotificationHandler> logger)
    {
        _logger = logger;
    }

    public void Handle(BookAdded evt)
    {
        _logger.LogInformation("Сповіщення читачам: у каталозі нова книга «{Title}», {Author}",
            evt.Title, evt.Author);
    }
}
