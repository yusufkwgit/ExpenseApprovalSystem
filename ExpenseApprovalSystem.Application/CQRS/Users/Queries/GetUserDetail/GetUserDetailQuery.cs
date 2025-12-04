using ExpenseApprovalSystem.Application.DTOs;
using ExpenseApprovalSystem.Application.Services.Interfaces;
using MediatR;

namespace ExpenseApprovalSystem.Application.CQRS.Users.Queries.GetUserDetail;

public sealed record GetUserDetailQuery(int UserId) : IRequest<UserDTO?>;

public sealed class GetUserDetailQueryHandler : IRequestHandler<GetUserDetailQuery, UserDTO?>
{
    private readonly IUserService _userService;

    public GetUserDetailQueryHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<UserDTO?> Handle(GetUserDetailQuery request, CancellationToken cancellationToken)
    {
        return await _userService.GetByIdAsync(request.UserId);
    }
}








