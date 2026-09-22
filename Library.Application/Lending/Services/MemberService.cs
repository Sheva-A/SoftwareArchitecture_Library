using Library.Application.Lending.DTOs;
using Library.Domain.Lending.Entities;
using Library.Domain.Lending.Repositories;

namespace Library.Application.Lending.Services;

// Реалізація сервісу читачів
public class MemberService : IMemberService
{
    private readonly IMemberRepository _memberRepository;

    public MemberService(IMemberRepository memberRepository)
    {
        _memberRepository = memberRepository;
    }

    public async Task<IEnumerable<MemberDto>> GetAllAsync()
    {
        var members = await _memberRepository.GetAllAsync();
        return members.Select(MapToDto);
    }

    public async Task<MemberDto?> GetByIdAsync(Guid id)
    {
        var member = await _memberRepository.GetByIdAsync(id);
        return member is null ? null : MapToDto(member);
    }

    public async Task<MemberDto> CreateAsync(CreateMemberDto dto)
    {
        // Валідація — у конструкторі Member
        var member = new Member(dto.FullName, dto.Email);
        await _memberRepository.AddAsync(member);
        return MapToDto(member);
    }

    private static MemberDto MapToDto(Member member) => new()
    {
        Id = member.Id,
        FullName = member.FullName,
        Email = member.Email,
        RegisteredAt = member.RegisteredAt
    };
}
