namespace Library.Application.Lending.DTOs;

// DTO для реєстрації нового читача
public class CreateMemberDto
{
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}
