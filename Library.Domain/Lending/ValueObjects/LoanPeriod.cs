using Library.Domain.Common;

namespace Library.Domain.Lending.ValueObjects;

// LoanPeriod — об'єкт-значення, що описує термін позики.
// Інкапсулює дати видачі та повернення разом із правилом визначення прострочення.
public sealed class LoanPeriod : ValueObject
{
    public DateTime BorrowedAt { get; }
    public DateTime DueDate { get; }

    // Обчислюване: чи є позика простроченою
    public bool IsOverdue => DateTime.UtcNow > DueDate;

    private LoanPeriod(DateTime borrowedAt, DateTime dueDate)
    {
        BorrowedAt = borrowedAt;
        DueDate = dueDate;
    }

    // За замовчуванням — 14 днів на читання
    public static LoanPeriod Create(DateTime borrowedAt, int durationDays = 14)
    {
        if (durationDays <= 0)
            throw new ArgumentException("Тривалість позики повинна бути більше нуля днів.");

        return new LoanPeriod(borrowedAt, borrowedAt.AddDays(durationDays));
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return BorrowedAt;
        yield return DueDate;
    }
}
