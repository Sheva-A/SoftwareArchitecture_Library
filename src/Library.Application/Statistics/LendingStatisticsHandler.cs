using Library.Application.Events;
using Library.Application.Lending.Events;
using Microsoft.Extensions.Logging;

namespace Library.Application.Statistics;

// Рахує, скільки разів видавали книги
public class LendingStatisticsHandler : IEventHandler<BookBorrowed>
{
    private readonly ILogger<LendingStatisticsHandler> _logger;
    private int _loansCount;

    public LendingStatisticsHandler(ILogger<LendingStatisticsHandler> logger)
    {
        _logger = logger;
    }

    public void Handle(BookBorrowed evt)
    {
        var count = Interlocked.Increment(ref _loansCount);
        _logger.LogInformation("Статистика: усього видач {Count}", count);
    }
}
