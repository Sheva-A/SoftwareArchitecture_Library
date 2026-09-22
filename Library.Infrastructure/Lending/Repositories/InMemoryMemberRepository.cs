using Library.Domain.Lending.Entities;
using Library.Domain.Lending.Repositories;

namespace Library.Infrastructure.Lending.Repositories;

// In-memory реалізація репозиторію читачів
public class InMemoryMemberRepository : IMemberRepository
{
    private readonly List<Member> _members = new();

    public Task<Member?> GetByIdAsync(Guid id)
        => Task.FromResult(_members.FirstOrDefault(m => m.Id == id));

    public Task<IEnumerable<Member>> GetAllAsync()
        => Task.FromResult<IEnumerable<Member>>(_members);

    public Task AddAsync(Member member)
    {
        _members.Add(member);
        return Task.CompletedTask;
    }
}
