using Library.Application.BookCatalog.DTOs;
using Library.Application.BookCatalog.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebApi.Controllers;

// Контролер для книг — делегує всю логіку сервісу IBookService
[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;

    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }

    // GET api/books
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookDto>>> GetAll()
    {
        var books = await _bookService.GetAllAsync();
        return Ok(books);
    }

    // GET api/books/available
    [HttpGet("available")]
    public async Task<ActionResult<IEnumerable<BookDto>>> GetAvailable()
    {
        var books = await _bookService.GetAvailableAsync();
        return Ok(books);
    }

    // GET api/books/{id}
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<BookDto>> GetById(Guid id)
    {
        var book = await _bookService.GetByIdAsync(id);
        return book is null ? NotFound() : Ok(book);
    }

    // POST api/books
    [HttpPost]
    public async Task<ActionResult<BookDto>> Create([FromBody] CreateBookDto dto)
    {
        try
        {
            var book = await _bookService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = book.Id }, book);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
