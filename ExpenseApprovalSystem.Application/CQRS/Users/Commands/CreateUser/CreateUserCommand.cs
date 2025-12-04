using ExpenseApprovalSystem.Application.DTOs;
using ExpenseApprovalSystem.Application.Services.Interfaces;
using MediatR;

namespace ExpenseApprovalSystem.Application.CQRS.Users.Commands.CreateUser;

public sealed record CreateUserCommand(CreateUserDTO Dto) : IRequest<UserDTO>;

public sealed class CreateUserCommandHandler
    : IRequestHandler<CreateUserCommand, UserDTO>
{
    private readonly IUserService _userService;

    public CreateUserCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<UserDTO> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        return await _userService.CreateAsync(request.Dto);
    }
}








