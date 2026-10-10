using Library.Application.Catalog;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebApi.Controllers;

// Контролер лише приймає запит і передає його в сервіс
[ApiController]
[Route("api/books")]
public class BooksController : ControllerBase
{
    private readonly BookService _bookService;

    public BooksController(BookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_bookService.GetAll());
    }

    [HttpPost]
    public IActionResult Add(AddBookRequest request)
    {
        var book = _bookService.AddBook(request.Title, request.Author, request.Isbn);
        return Ok(book);
    }
}

// Дані для додавання книги
public record AddBookRequest(string Title, string Author, string Isbn);
