using Library.Domain.Lending;

namespace Library.Application.Lending;

// Абстракція сервісу видачі, від неї залежить контролер
public interface ILoanService
{
    Loan BorrowBook(Guid bookId, string readerName);
    Loan ReturnBook(Guid loanId);
    IReadOnlyList<Loan> GetAll();
}
