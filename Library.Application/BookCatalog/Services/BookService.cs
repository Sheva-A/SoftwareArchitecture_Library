using Library.Application.BookCatalog.DTOs;
using Library.Domain.BookCatalog.Entities;
using Library.Domain.BookCatalog.Repositories;
using Library.Domain.BookCatalog.ValueObjects;

namespace Library.Application.BookCatalog.Services;

// Реалізація сервісу книг — розміщується в Application шарі.
// Оркеструє доменні об'єкти, не містить бізнес-правил (вони у Book).
public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly IAuthorRepository _authorRepository;

    public BookService(IBookRepository bookRepository, IAuthorRepository authorRepository)
    {
        _bookRepository = bookRepository;
        _authorRepository = authorRepository;
    }

    public async Task<IEnumerable<BookDto>> GetAllAsync()
    {
        var books = await _bookRepository.GetAllAsync();
        return await MapToDtosAsync(books);
    }

    public async Task<IEnumerable<BookDto>> GetAvailableAsync()
    {
        var books = await _bookRepository.GetAvailableAsync();
        return await MapToDtosAsync(books);
    }

    public async Task<BookDto?> GetByIdAsync(Guid id)
    {
        var book = await _bookRepository.GetByIdAsync(id);
        if (book is null) return null;

        var author = await _authorRepository.GetByIdAsync(book.AuthorId);
        return MapToDto(book, author?.FullName ?? "Невідомо");
    }

    public async Task<BookDto> CreateAsync(CreateBookDto dto)
    {
        // Перевірити існування автора
        var author = await _authorRepository.GetByIdAsync(dto.AuthorId)
            ?? throw new ArgumentException($"Автора з Id {dto.AuthorId} не знайдено.");

        // Створення Value Objects викликає їхню валідацію
        var title = BookTitle.Create(dto.Title);
        var isbn = ISBN.Create(dto.Isbn);

        // Створення доменної сутності — бізнес-правила у конструкторі Book
        var book = new Book(title, isbn, dto.AuthorId, dto.PublicationYear);

        await _bookRepository.AddAsync(book);

        return MapToDto(book, author.FullName);
    }

    // Допоміжний метод для маппінгу списку книг у DTO
    private async Task<IEnumerable<BookDto>> MapToDtosAsync(IEnumerable<Book> books)
    {
        var authors = (await _authorRepository.GetAllAsync())
            .ToDictionary(a => a.Id, a => a.FullName);

        return books.Select(b => MapToDto(b,
            authors.TryGetValue(b.AuthorId, out var name) ? name : "Невідомо"));
    }

    private static BookDto MapToDto(Book book, string authorName) => new()
    {
        Id = book.Id,
        Title = book.Title.Value,
        Isbn = book.Isbn.Value,
        AuthorName = authorName,
        PublicationYear = book.PublicationYear,
        IsAvailable = book.IsAvailable
    };
}
