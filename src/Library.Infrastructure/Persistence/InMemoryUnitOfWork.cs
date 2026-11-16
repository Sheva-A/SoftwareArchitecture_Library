using Library.Application.Common;
using Library.Domain.Catalog;
using Library.Domain.Lending;

namespace Library.Infrastructure.Persistence;

// Накопичує зміни поточного запиту і зберігає їх у сховище разом
public class InMemoryUnitOfWork : IUnitOfWork
{
    private readonly InMemoryStore _store;

    // Нові та змінені об'єкти, які ще не збережені
    public Dictionary<Guid, Book> Books { get; } = new();
    public Dictionary<Guid, Loan> Loans { get; } = new();

    public InMemoryUnitOfWork(InMemoryStore store)
    {
        _store = store;
    }

    public void SaveChanges()
    {
        // У сховище записуємо копії, щоб подальші зміни об'єктів його не зачіпали
        foreach (var book in Books.Values)
            _store.Books[book.Id] = EntityCopy.Of(book);

        foreach (var loan in Loans.Values)
            _store.Loans[loan.Id] = EntityCopy.Of(loan);
    }
}
