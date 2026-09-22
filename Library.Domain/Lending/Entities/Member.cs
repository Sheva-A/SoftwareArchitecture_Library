using Library.Domain.Common;

namespace Library.Domain.Lending.Entities;

// Member — сутність читача у bounded context Lending.
// У контексті BookCatalog поняття "користувач" відсутнє —
// це приклад того, як різні BC мають власні моделі.
public class Member : Entity
{
    public string FullName { get; private set; }
    public string Email { get; private set; }
    public DateTime RegisteredAt { get; private set; }

    // Приватний конструктор для ORM
    private Member() { FullName = ""; Email = ""; }

    public Member(string fullName, string email)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentException("Ім'я читача не може бути порожнім.");

        if (string.IsNullOrWhiteSpace(email) || !email.Contains('@'))
            throw new ArgumentException("Email читача некоректний.");

        FullName = fullName.Trim();
        Email = email.Trim().ToLowerInvariant();
        RegisteredAt = DateTime.UtcNow;
    }
}
