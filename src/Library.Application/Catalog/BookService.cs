using Library.Application.Catalog.Events;
using Library.Application.Common;
using Library.Application.Events;
using Library.Domain.Catalog;

namespace Library.Application.Catalog;

// Сценарії роботи з каталогом книг: додавання та перегляд
public class BookService : IBookService
{
    private readonly IBookRepository _books;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventBus _events;
    private readonly IBookMapper _mapper;
    private readonly IBookListCache _cache;

    public BookService(IBookRepository books, IUnitOfWork unitOfWork, IEventBus events, IBookMapper mapper,
        IBookListCache cache)
    {
        _books = books;
        _unitOfWork = unitOfWork;
        _events = events;
        _mapper = mapper;
        _cache = cache;
    }

    public AddBookResultDto AddBook(AddBookDto dto)
    {
        // Перевірки виконує сама доменна модель
        var book = new Book(dto.Title, dto.Author, new Isbn(dto.Isbn));
        _books.Add(book);

        // Зберігаємо зміни через Unit of Work
        _unitOfWork.SaveChanges();

        // Список книг змінився, тому видаляємо його з кешу
        _cache.Remove();

        // Публікуємо подію, не знаючи, хто її обробить
        _events.Publish(new BookAdded(book.Id, book.Title, book.Author));

        // Назовні повертаємо DTO, а не доменну сутність
        return _mapper.ToAddResultDto(book);
    }

    public IReadOnlyList<BookDto> GetAll()
    {
        // Спочатку пробуємо взяти список з кешу
        var cached = _cache.Get();
        if (cached != null)
            return cached;

        // Якщо в кеші немає, беремо дані зі сховища і зберігаємо в кеш
        var books = _books.GetAll().Select(_mapper.ToDto).ToList();
        _cache.Set(books);

        return books;
    }
}