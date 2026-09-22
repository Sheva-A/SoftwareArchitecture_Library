using Library.Domain.Common;
using Library.Domain.Lending.ValueObjects;

namespace Library.Domain.Lending.Entities;

// Loan — коренева сутність bounded context Lending.
// Містить бізнес-правила оформлення та повернення позики.
public class Loan : Entity
{
    // BookId — посилання на книгу з іншого bounded context (BookCatalog).
    // Зберігаємо лише Id, а не саму сутність Book — це cross-context зв'язок через ідентифікатор.
    public Guid BookId { get; private set; }
    public Guid MemberId { get; private set; }

    public LoanPeriod Period { get; private set; }

    // null означає, що книгу ще не повернули
    public DateTime? ReturnedAt { get; private set; }
    public bool IsReturned => ReturnedAt.HasValue;

    // Приватний конструктор для ORM
    private Loan() { Period = null!; }

    public Loan(Guid bookId, Guid memberId, LoanPeriod period)
    {
        if (bookId == Guid.Empty)
            throw new ArgumentException("BookId не може бути порожнім.");

        if (memberId == Guid.Empty)
            throw new ArgumentException("MemberId не може бути порожнім.");

        BookId = bookId;
        MemberId = memberId;
        Period = period ?? throw new ArgumentNullException(nameof(period));
    }

    // Бізнес-правило: повернути книгу можна лише один раз
    public void Return()
    {
        if (IsReturned)
            throw new InvalidOperationException("Позику вже закрито — книгу вже повернуто.");

        ReturnedAt = DateTime.UtcNow;
    }
}
