using Library.Domain.Catalog;
using Library.Domain.Lending;

namespace Library.Infrastructure.Persistence;

// Збережені дані застосунку, зберігаються в пам'яті
public class InMemoryStore
{
    public Dictionary<Guid, Book> Books { get; } = new();
    public Dictionary<Guid, Loan> Loans { get; } = new();
}
