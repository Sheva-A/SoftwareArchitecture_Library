using Library.Domain.Lending.Entities;

namespace Library.Domain.Lending.Repositories;

// Інтерфейс репозиторію читачів — оголошується в доменному шарі.
public interface IMemberRepository
{
    Task<Member?> GetByIdAsync(Guid id);
    Task<IEnumerable<Member>> GetAllAsync();
    Task AddAsync(Member member);
}
