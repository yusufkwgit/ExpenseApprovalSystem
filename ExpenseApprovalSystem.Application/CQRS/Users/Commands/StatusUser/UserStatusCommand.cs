using ExpenseApprovalSystem.Application.Services.Interfaces;
using MediatR;

namespace ExpenseApprovalSystem.Application.CQRS.Users.Commands.DeactivateUser;

public sealed record UserStatusCommand(int UserId) : IRequest;

public sealed class DeactivateUserCommandHandler : IRequestHandler<UserStatusCommand>
{
    private readonly IUserService _userService;

    public DeactivateUserCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task Handle(UserStatusCommand request, CancellationToken cancellationToken)
    {
        await _userService.UserStatus(request.UserId);
    }
}




