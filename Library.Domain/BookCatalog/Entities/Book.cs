using Library.Domain.Common;
using Library.Domain.BookCatalog.ValueObjects;

namespace Library.Domain.BookCatalog.Entities;

// Book — коренева сутність (Aggregate Root) bounded context BookCatalog.
// Містить бізнес-правила видачі та повернення книги.
public class Book : Entity
{
    public BookTitle Title { get; private set; }
    public ISBN Isbn { get; private set; }
    public Guid AuthorId { get; private set; }
    public int PublicationYear { get; private set; }

    // Прапорець доступності — відображає поточний стан книги у бібліотеці
    public bool IsAvailable { get; private set; }

    // Приватний конструктор для ORM
    private Book() { Title = null!; Isbn = null!; }

    public Book(BookTitle title, ISBN isbn, Guid authorId, int publicationYear)
    {
        Title = title ?? throw new ArgumentNullException(nameof(title));
        Isbn = isbn ?? throw new ArgumentNullException(nameof(isbn));

        if (authorId == Guid.Empty)
            throw new ArgumentException("AuthorId не може бути порожнім.");

        if (publicationYear < 1 || publicationYear > DateTime.UtcNow.Year)
            throw new ArgumentException("Рік видання некоректний.");

        AuthorId = authorId;
        PublicationYear = publicationYear;
        IsAvailable = true; // нова книга завжди доступна
    }

    // Бізнес-правило: книгу можна видати лише якщо вона доступна
    public void CheckOut()
    {
        if (!IsAvailable)
            throw new InvalidOperationException("Книга вже видана читачу.");

        IsAvailable = false;
    }

    // Бізнес-правило: повернути можна лише видану книгу
    public void Return()
    {
        if (IsAvailable)
            throw new InvalidOperationException("Книга не була видана.");

        IsAvailable = true;
    }
}
