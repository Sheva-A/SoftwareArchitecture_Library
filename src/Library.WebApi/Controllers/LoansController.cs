using Library.Application.Lending;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebApi.Controllers;

// Контролер видачі та повернення книг
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
    public IActionResult Borrow(BorrowBookRequest request)
    {
        var loan = _loanService.BorrowBook(request.BookId, request.ReaderName);
        return Ok(loan);
    }

    [HttpPost("{id}/return")]
    public IActionResult Return(Guid id)
    {
        var loan = _loanService.ReturnBook(id);
        return Ok(loan);
    }
}

// Дані для видачі книги
public record BorrowBookRequest(Guid BookId, string ReaderName);
