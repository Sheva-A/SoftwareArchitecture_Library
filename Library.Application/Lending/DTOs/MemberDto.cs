namespace Library.Application.Lending.DTOs;

// DTO для відображення читача
public class MemberDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime RegisteredAt { get; set; }
}
