namespace Library.Application.Lending;

// Абстракція сервісу видачі, приймає і повертає лише DTO
public interface ILoanService
{
    BorrowBookResultDto BorrowBook(BorrowBookDto dto);
    ReturnBookResultDto ReturnBook(Guid loanId);
    IReadOnlyList<LoanDto> GetAll();
}
