using Library.Application.Lending.DTOs;
using Library.Application.Lending.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebApi.Controllers;

// Контролер для позик (bounded context Lending)
[ApiController]
[Route("api/[controller]")]
public class LoansController : ControllerBase
{
    private readonly ILoanService _loanService;

    public LoansController(ILoanService loanService)
    {
        _loanService = loanService;
    }

    // GET api/loans
    [HttpGet]
    public async Task<ActionResult<IEnumerable<LoanDto>>> GetAll()
    {
        var loans = await _loanService.GetAllAsync();
        return Ok(loans);
    }

    // GET api/loans/active
    [HttpGet("active")]
    public async Task<ActionResult<IEnumerable<LoanDto>>> GetActive()
    {
        var loans = await _loanService.GetActiveAsync();
        return Ok(loans);
    }

    // GET api/loans/{id}
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<LoanDto>> GetById(Guid id)
    {
        var loan = await _loanService.GetByIdAsync(id);
        return loan is null ? NotFound() : Ok(loan);
    }

    // POST api/loans — видати книгу читачу
    [HttpPost]
    public async Task<ActionResult<LoanDto>> BorrowBook([FromBody] CreateLoanDto dto)
    {
        try
        {
            var loan = await _loanService.BorrowBookAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = loan.Id }, loan);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
    }

    // PUT api/loans/{id}/return — повернути книгу
    [HttpPut("{id:guid}/return")]
    public async Task<ActionResult<LoanDto>> ReturnBook(Guid id)
    {
        try
        {
            var loan = await _loanService.ReturnBookAsync(id);
            return Ok(loan);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
    }
}
