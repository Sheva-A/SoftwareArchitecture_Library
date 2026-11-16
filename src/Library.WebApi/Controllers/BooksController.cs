using Library.Application.Catalog;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebApi.Controllers;

// Контролер приймає і повертає лише DTO, доменних сутностей тут немає
[ApiController]
[Route("api/books")]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;

    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_bookService.GetAll());
    }

    [HttpPost]
    public IActionResult Add(AddBookDto dto)
    {
        return Ok(_bookService.AddBook(dto));
    }
}
