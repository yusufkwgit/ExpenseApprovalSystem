using ExpenseApprovalSystem.Application.DTOs;
using ExpenseApprovalSystem.Application.Services.Interfaces;
using MediatR;

namespace ExpenseApprovalSystem.Application.CQRS.Users.Queries.GetAllUsers;

public sealed record GetAllUsersQuery : IRequest<IReadOnlyList<UserDTO>>;

public sealed class GetAllUsersQueryHandler
    : IRequestHandler<GetAllUsersQuery, IReadOnlyList<UserDTO>>
{
    private readonly IUserService _userService;

    public GetAllUsersQueryHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<IReadOnlyList<UserDTO>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        return await _userService.GetAllAsync();
    }
}








