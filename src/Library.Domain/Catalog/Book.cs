using Library.Domain.Common;

namespace Library.Domain.Catalog;

// Сутність: книга в каталозі бібліотеки
public class Book
{
    public Guid Id { get; }
    public string Title { get; }
    public string Author { get; }
    public Isbn Isbn { get; }
    public bool IsAvailable { get; private set; }

    public Book(string title, string author, Isbn isbn)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new DomainException("Назва книги обов'язкова");

        if (string.IsNullOrWhiteSpace(author))
            throw new DomainException("Автор книги обов'язковий");

        Id = Guid.NewGuid();
        Title = title.Trim();
        Author = author.Trim();
        Isbn = isbn;

        // Нова книга одразу доступна для видачі
        IsAvailable = true;
    }

    // Книгу видали читачу
    public void MarkAsBorrowed()
    {
        if (!IsAvailable)
            throw new DomainException("Книга вже видана");

        IsAvailable = false;
    }

    // Книгу повернули до бібліотеки
    public void MarkAsReturned()
    {
        if (IsAvailable)
            throw new DomainException("Книга вже є в бібліотеці");

        IsAvailable = true;
    }
}
