using ExpenseApprovalSystem.Application.CQRS.Users.Commands.CreateUser;
using ExpenseApprovalSystem.Application.CQRS.Users.Commands.DeactivateUser;
using ExpenseApprovalSystem.Application.CQRS.Users.Queries.GetAllUsers;
using ExpenseApprovalSystem.Application.CQRS.Users.Queries.GetUserDetail;
using ExpenseApprovalSystem.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseApprovalSystem.API.Controllers;

[ApiController]
[Route("api/users")]
public sealed class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<UserDTO>>> GetAsync()
    {
        var users = await _mediator.Send(new GetAllUsersQuery());
        return Ok(users);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<UserDTO>> GetByIdAsync(int id)
    {
        var user = await _mediator.Send(new GetUserDetailQuery(id));
        if (user is null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult<UserDTO>> CreateAsync([FromBody] CreateUserDTO dto)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var created = await _mediator.Send(new CreateUserCommand(dto));
        return CreatedAtRoute(nameof(GetByIdAsync), new { id = created.UserID }, created);
    }

    [HttpPost("{id:int}/user-status-change")]
    public async Task<IActionResult> DeactivateAsync(int id)
    {
        await _mediator.Send(new UserStatusCommand(id));
        return NoContent();
    }

}



