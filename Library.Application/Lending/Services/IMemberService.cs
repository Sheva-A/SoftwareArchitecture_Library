using Library.Application.Lending.DTOs;

namespace Library.Application.Lending.Services;

// Інтерфейс сервісу читачів
public interface IMemberService
{
    Task<IEnumerable<MemberDto>> GetAllAsync();
    Task<MemberDto?> GetByIdAsync(Guid id);
    Task<MemberDto> CreateAsync(CreateMemberDto dto);
}
