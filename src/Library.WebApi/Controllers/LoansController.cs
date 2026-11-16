using Library.Application.Lending;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebApi.Controllers;

// Контролер видачі та повернення книг, працює лише з DTO
[ApiController]
[Route("api/loans")]
public class LoansController : ControllerBase
{
    private readonly ILoanService _loanService;

    public LoansController(ILoanService loanService)
    {
        _loanService = loanService;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_loanService.GetAll());
    }

    [HttpPost]
    public IActionResult Borrow(BorrowBookDto dto)
    {
        return Ok(_loanService.BorrowBook(dto));
    }

    [HttpPost("{id}/return")]
    public IActionResult Return(Guid id)
    {
        return Ok(_loanService.ReturnBook(id));
    }
}
