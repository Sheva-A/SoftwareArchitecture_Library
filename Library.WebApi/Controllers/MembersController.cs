using Library.Application.Lending.DTOs;
using Library.Application.Lending.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebApi.Controllers;

// Контролер для читачів (bounded context Lending)
[ApiController]
[Route("api/[controller]")]
public class MembersController : ControllerBase
{
    private readonly IMemberService _memberService;

    public MembersController(IMemberService memberService)
    {
        _memberService = memberService;
    }

    // GET api/members
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetAll()
    {
        var members = await _memberService.GetAllAsync();
        return Ok(members);
    }

    // GET api/members/{id}
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<MemberDto>> GetById(Guid id)
    {
        var member = await _memberService.GetByIdAsync(id);
        return member is null ? NotFound() : Ok(member);
    }

    // POST api/members
    [HttpPost]
    public async Task<ActionResult<MemberDto>> Create([FromBody] CreateMemberDto dto)
    {
        try
        {
            var member = await _memberService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = member.Id }, member);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
