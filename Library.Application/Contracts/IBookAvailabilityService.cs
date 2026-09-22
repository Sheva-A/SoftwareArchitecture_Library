namespace Library.Application.Contracts;

// Контракт між двома bounded contexts: Lending та BookCatalog.
// Lending використовує цей інтерфейс, не знаючи деталей реалізації BookCatalog.
// Реалізація знаходиться в Infrastructure і звертається до BookCatalog репозиторію.
public interface IBookAvailabilityService
{
    // Перевірити, чи книга доступна для видачі
    Task<bool> IsBookAvailableAsync(Guid bookId);

    // Позначити книгу як видану (викликається після оформлення позики)
    Task MarkBookAsCheckedOutAsync(Guid bookId);

    // Повернути книгу до каталогу як доступну
    Task MarkBookAsReturnedAsync(Guid bookId);
}
