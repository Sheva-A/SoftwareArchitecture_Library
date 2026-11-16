using System.Text.Json;
using Enyim.Caching;
using Library.Application.Catalog;
using Microsoft.Extensions.Logging;

namespace Library.Infrastructure.Caching;

// Зберігає список книг у Memcached
public class MemcachedBookListCache : IBookListCache
{
    private const string Key = "books:all";

    // Час життя кешу (TTL) у секундах
    private const int TtlSeconds = 60;

    private readonly IMemcachedClient _client;
    private readonly ILogger<MemcachedBookListCache> _logger;

    public MemcachedBookListCache(IMemcachedClient client, ILogger<MemcachedBookListCache> logger)
    {
        _client = client;
        _logger = logger;
    }

    public IReadOnlyList<BookDto>? Get()
    {
        // Якщо запису немає або Memcached недоступний, отримаємо null
        var json = _client.Get<string>(Key);
        if (json == null)
        {
            _logger.LogInformation("Кеш: списку книг у кеші немає");
            return null;
        }

        _logger.LogInformation("Кеш: список книг отримано з Memcached");
        return JsonSerializer.Deserialize<List<BookDto>>(json);
    }

    public void Set(IReadOnlyList<BookDto> books)
    {
        var saved = _client.Set(Key, JsonSerializer.Serialize(books), TtlSeconds);

        if (saved)
            _logger.LogInformation("Кеш: список книг збережено на {Ttl} секунд", TtlSeconds);
        else
            _logger.LogWarning("Кеш: Memcached недоступний, працюємо без кешу");
    }

    public void Remove()
    {
        _client.Remove(Key);
        _logger.LogInformation("Кеш: список книг видалено після зміни даних");
    }
}
