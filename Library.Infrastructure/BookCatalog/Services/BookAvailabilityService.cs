using Library.Application.Contracts;
using Library.Domain.BookCatalog.Repositories;

namespace Library.Infrastructure.BookCatalog.Services;

// Реалізація cross-context контракту IBookAvailabilityService.
// Знаходиться в Infrastructure, тому що тут дозволені залежності від двох BC.
// Lending BC використовує лише інтерфейс — не знає про цю реалізацію.
public class BookAvailabilityService : IBookAvailabilityService
{
    private readonly IBookRepository _bookRepository;

    public BookAvailabilityService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<bool> IsBookAvailableAsync(Guid bookId)
    {
        var book = await _bookRepository.GetByIdAsync(bookId);
        return book?.IsAvailable ?? false;
    }

    public async Task MarkBookAsCheckedOutAsync(Guid bookId)
    {
        var book = await _bookRepository.GetByIdAsync(bookId)
            ?? throw new ArgumentException($"Книгу з Id {bookId} не знайдено.");

        // Виклик доменного методу — бізнес-правило у самій книзі
        book.CheckOut();
        await _bookRepository.UpdateAsync(book);
    }

    public async Task MarkBookAsReturnedAsync(Guid bookId)
    {
        var book = await _bookRepository.GetByIdAsync(bookId)
            ?? throw new ArgumentException($"Книгу з Id {bookId} не знайдено.");

        book.Return();
        await _bookRepository.UpdateAsync(book);
    }
}
