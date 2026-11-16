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

    public BookService(IBookRepository books, IUnitOfWork unitOfWork, IEventBus events, IBookMapper mapper)
    {
        _books = books;
        _unitOfWork = unitOfWork;
        _events = events;
        _mapper = mapper;
    }

    public AddBookResultDto AddBook(AddBookDto dto)
    {
        // Перевірки виконує сама доменна модель
        var book = new Book(dto.Title, dto.Author, new Isbn(dto.Isbn));
        _books.Add(book);

        // Зберігаємо зміни через Unit of Work
        _unitOfWork.SaveChanges();

        // Публікуємо подію, не знаючи, хто її обробить
        _events.Publish(new BookAdded(book.Id, book.Title, book.Author));

        // Назовні повертаємо DTO, а не доменну сутність
        return _mapper.ToAddResultDto(book);
    }

    public IReadOnlyList<BookDto> GetAll()
    {
        return _books.GetAll().Select(_mapper.ToDto).ToList();
    }
}