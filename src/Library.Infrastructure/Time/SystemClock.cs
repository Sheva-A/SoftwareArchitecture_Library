using Library.Domain.Common;

namespace Library.Infrastructure.Time;

// Повертає системний час комп'ютера
public class SystemClock : IClock
{
    public DateTime Now => DateTime.Now;
}
