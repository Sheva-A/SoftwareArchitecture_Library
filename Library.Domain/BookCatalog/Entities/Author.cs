using Library.Domain.Common;

namespace Library.Domain.BookCatalog.Entities;

// Author — сутність у межах bounded context BookCatalog.
// Представляє автора книги разом із бізнес-правилами валідації імені.
public class Author : Entity
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }

    // Обчислюване властивість — не зберігається окремо
    public string FullName => $"{FirstName} {LastName}";

    // Приватний конструктор для ORM (Entity Framework тощо)
    private Author() { FirstName = ""; LastName = ""; }

    public Author(string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("Ім'я автора не може бути порожнім.");

        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Прізвище автора не може бути порожнім.");

        FirstName = firstName.Trim();
        LastName = lastName.Trim();
    }
}
