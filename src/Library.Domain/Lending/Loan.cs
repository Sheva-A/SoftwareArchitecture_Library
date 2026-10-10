using Library.Domain.Common;

namespace Library.Domain.Lending;

// Сутність: видача книги читачу
public class Loan
{
    // Строк, на який видається книга
    private const int LoanDays = 14;

    public Guid Id { get; }

    // Зберігаємо лише Id книги, а не саму книгу з каталогу
    public Guid BookId { get; }
    public string ReaderName { get; }
    public DateTime LoanDate { get; }
    public DateTime DueDate { get; }
    public DateTime? ReturnDate { get; private set; }
    public bool IsReturned => ReturnDate != null;

    public Loan(Guid bookId, string readerName)
    {
        if (string.IsNullOrWhiteSpace(readerName))
            throw new DomainException("Ім'я читача обов'язкове");

        Id = Guid.NewGuid();
        BookId = bookId;
        ReaderName = readerName.Trim();
        LoanDate = DateTime.Now;
        DueDate = LoanDate.AddDays(LoanDays);
    }

    // Читач повертає книгу
    public void Return()
    {
        if (IsReturned)
            throw new DomainException("Книгу вже повернули");

        ReturnDate = DateTime.Now;
    }
}
