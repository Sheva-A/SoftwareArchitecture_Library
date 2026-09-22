namespace Library.Application.Lending.DTOs;

// DTO для запиту на видачу книги читачу
public class CreateLoanDto
{
    public Guid BookId { get; set; }
    public Guid MemberId { get; set; }
}
