namespace Library.Application.Lending.DTOs;

// DTO для відображення позики
public class LoanDto
{
    public Guid Id { get; set; }
    public Guid BookId { get; set; }
    public Guid MemberId { get; set; }
    public string MemberName { get; set; } = string.Empty;
    public DateTime BorrowedAt { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? ReturnedAt { get; set; }
    public bool IsReturned { get; set; }
    public bool IsOverdue { get; set; }
}
