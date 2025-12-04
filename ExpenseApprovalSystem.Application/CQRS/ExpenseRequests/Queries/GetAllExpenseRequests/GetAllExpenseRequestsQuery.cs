using AutoMapper;
using ExpenseApprovalSystem.Application.DTOs;
using ExpenseApprovalSystem.Application.Interfaces;
using ExpenseApprovalSystem.Application.Services.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpenseApprovalSystem.Application.CQRS.ExpenseRequests.Queries.GetAllExpenseRequests;

public sealed record GetAllExpenseRequestsQuery : IRequest<IReadOnlyList<ExpenseRequestListDTO>>;

public sealed class GetAllExpenseRequestsQueryHandler
    : IRequestHandler<GetAllExpenseRequestsQuery, IReadOnlyList<ExpenseRequestListDTO>>
{
    private readonly IExpenseRequestService _expenseRequestService;

    public GetAllExpenseRequestsQueryHandler(IExpenseRequestService expenseRequest)
    {
        _expenseRequestService = expenseRequest;
    }

    public async Task<IReadOnlyList<ExpenseRequestListDTO>> Handle(GetAllExpenseRequestsQuery request, CancellationToken cancellationToken)
    {
        return await _expenseRequestService.GetAllAsync();
    }
}