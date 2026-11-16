using Library.Domain.Lending;

namespace Library.Application.Lending;

// Мапінг доменної сутності Loan у DTO
public interface ILoanMapper
{
    BorrowBookResultDto ToBorrowResultDto(Loan loan);
    ReturnBookResultDto ToReturnResultDto(Loan loan);
    LoanDto ToDto(Loan loan);
}
