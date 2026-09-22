using Library.Domain.Common;

namespace Library.Domain.BookCatalog.ValueObjects;

// BookTitle — об'єкт-значення для назви книги.
// Інкапсулює бізнес-правило: назва не може бути порожньою і не довше 200 символів.
public sealed class BookTitle : ValueObject
{
    public string Value { get; }

    private BookTitle(string value)
    {
        Value = value;
    }

    public static BookTitle Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Назва книги не може бути порожньою.");

        if (value.Length > 200)
            throw new ArgumentException("Назва книги не може перевищувати 200 символів.");

        return new BookTitle(value.Trim());
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;
}
