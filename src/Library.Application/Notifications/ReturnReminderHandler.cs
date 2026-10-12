using Library.Application.Events;
using Library.Application.Lending.Events;
using Microsoft.Extensions.Logging;

namespace Library.Application.Notifications;

// Нагадує читачу, до якої дати повернути книгу
public class ReturnReminderHandler : IEventHandler<BookBorrowed>
{
    private readonly ILogger<ReturnReminderHandler> _logger;

    public ReturnReminderHandler(ILogger<ReturnReminderHandler> logger)
    {
        _logger = logger;
    }

    public void Handle(BookBorrowed evt)
    {
        _logger.LogInformation("Нагадування для {Reader}: поверніть книгу до {DueDate}",
            evt.ReaderName, evt.DueDate.ToString("dd.MM.yyyy"));
    }
}
