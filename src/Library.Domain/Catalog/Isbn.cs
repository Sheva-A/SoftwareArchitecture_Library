using Library.Domain.Common;

namespace Library.Domain.Catalog;

// Об'єкт-значення: номер ISBN книги
public record Isbn
{
    public string Value { get; }

    public Isbn(string value)
    {
        // Прибираємо дефіси та пробіли
        var digits = (value ?? "").Replace("-", "").Replace(" ", "");

        // ISBN має містити 10 або 13 цифр
        if ((digits.Length != 10 && digits.Length != 13) || !digits.All(char.IsDigit))
            throw new DomainException("ISBN має містити 10 або 13 цифр");

        Value = digits;
    }
}
