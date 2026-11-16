using Library.Domain.Lending;

namespace Library.Application.Lending;

// Перетворює видачу в DTO вручну, поле за полем
public class LoanMapper : ILoanMapper
{
    public BorrowBookResultDto ToBorrowResultDto(Loan loan)
    {
        return new BorrowBookResultDto(loan.Id, loan.BookId, loan.ReaderName, loan.DueDate);
    }

    public ReturnBookResultDto ToReturnResultDto(Loan loan)
    {
        return new ReturnBookResultDto(loan.Id, loan.BookId, loan.ReturnDate);
    }

    public LoanDto ToDto(Loan loan)
    {
        return new LoanDto(loan.Id, loan.BookId, loan.ReaderName, loan.LoanDate, loan.DueDate,
            loan.ReturnDate);
    }
}
