using Library.Domain.Common;

namespace Library.Domain.BookCatalog.ValueObjects;

// ISBN — об'єкт-значення, що представляє міжнародний стандартний номер книги.
// Містить бізнес-правило валідації: ISBN повинен мати 10 або 13 цифр.
public sealed class ISBN : ValueObject
{
    public string Value { get; }

    private ISBN(string value)
    {
        Value = value;
    }

    // Фабричний метод — єдиний спосіб створити ISBN.
    // Гарантує, що об'єкт завжди перебуває у валідному стані.
    public static ISBN Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("ISBN не може бути порожнім.");

        var digits = value.Replace("-", "").Replace(" ", "");

        if (digits.Length != 10 && digits.Length != 13)
            throw new ArgumentException("ISBN повинен містити 10 або 13 цифр.");

        return new ISBN(value.Trim());
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;
}
