using Library.Application.BookCatalog.DTOs;
using Library.Application.BookCatalog.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebApi.Controllers;

// Контролер для авторів — лише маршрутизація та виклик сервісу.
// Бізнес-логіки у контролері немає (дотримання методичних вказівок).
[ApiController]
[Route("api/[controller]")]
public class AuthorsController : ControllerBase
{
    private readonly IAuthorService _authorService;

    public AuthorsController(IAuthorService authorService)
    {
        _authorService = authorService;
    }

    // GET api/authors
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AuthorDto>>> GetAll()
    {
        var authors = await _authorService.GetAllAsync();
        return Ok(authors);
    }

    // GET api/authors/{id}
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<AuthorDto>> GetById(Guid id)
    {
        var author = await _authorService.GetByIdAsync(id);
        return author is null ? NotFound() : Ok(author);
    }

    // POST api/authors
    [HttpPost]
    public async Task<ActionResult<AuthorDto>> Create([FromBody] CreateAuthorDto dto)
    {
        try
        {
            var author = await _authorService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = author.Id }, author);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
