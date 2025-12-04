using AutoMapper;
using ExpenseApprovalSystem.Application.DTOs;
using ExpenseApprovalSystem.Application.Interfaces;
using ExpenseApprovalSystem.Application.Services.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpenseApprovalSystem.Application.CQRS.ExpenseRequests.Queries.GetExpenseRequestsByEmployee;

public sealed record GetExpenseRequestsByEmployeeQuery(int EmployeeId)
    : IRequest<IReadOnlyList<ExpenseRequestListDTO>>;

public sealed class GetExpenseRequestsByEmployeeQueryHandler
    : IRequestHandler<GetExpenseRequestsByEmployeeQuery, IReadOnlyList<ExpenseRequestListDTO>>
{
    private readonly IExpenseRequestService _expenseRequestService;

    public GetExpenseRequestsByEmployeeQueryHandler(IExpenseRequestService expenseRequestService)
    {
        _expenseRequestService = expenseRequestService;
    }

    public async Task<IReadOnlyList<ExpenseRequestListDTO>> Handle(GetExpenseRequestsByEmployeeQuery request, CancellationToken cancellationToken)
    {
        return await _expenseRequestService.GetByEmployeeAsync(request.EmployeeId);
    }
}

