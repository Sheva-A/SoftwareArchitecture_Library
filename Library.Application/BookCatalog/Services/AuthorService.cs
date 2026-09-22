using Library.Application.BookCatalog.DTOs;
using Library.Domain.BookCatalog.Entities;
using Library.Domain.BookCatalog.Repositories;

namespace Library.Application.BookCatalog.Services;

// Реалізація сервісу авторів
public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository _authorRepository;

    public AuthorService(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }

    public async Task<IEnumerable<AuthorDto>> GetAllAsync()
    {
        var authors = await _authorRepository.GetAllAsync();
        return authors.Select(MapToDto);
    }

    public async Task<AuthorDto?> GetByIdAsync(Guid id)
    {
        var author = await _authorRepository.GetByIdAsync(id);
        return author is null ? null : MapToDto(author);
    }

    public async Task<AuthorDto> CreateAsync(CreateAuthorDto dto)
    {
        // Валідація — у конструкторі Author
        var author = new Author(dto.FirstName, dto.LastName);
        await _authorRepository.AddAsync(author);
        return MapToDto(author);
    }

    private static AuthorDto MapToDto(Author author) => new()
    {
        Id = author.Id,
        FullName = author.FullName
    };
}
