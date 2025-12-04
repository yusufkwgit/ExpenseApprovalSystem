using AutoMapper;
using ExpenseApprovalSystem.Application.DTOs;
using ExpenseApprovalSystem.Application.Interfaces;
using ExpenseApprovalSystem.Application.Services.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpenseApprovalSystem.Application.CQRS.ExpenseRequests.Queries.GetExpenseRequestDetail;

public sealed record GetExpenseRequestDetailQuery(int ExpenseRequestId)
    : IRequest<ExpenseRequestDTO?>;

public sealed class GetExpenseRequestDetailQueryHandler
    : IRequestHandler<GetExpenseRequestDetailQuery, ExpenseRequestDTO?>
{
    private readonly IExpenseRequestService _expenseRequestService;

    public GetExpenseRequestDetailQueryHandler(IExpenseRequestService expenseRequestService)
    {
        _expenseRequestService = expenseRequestService;
    }

    public async Task<ExpenseRequestDTO?> Handle(GetExpenseRequestDetailQuery request, CancellationToken cancellationToken)
    {
        return await _expenseRequestService.GetDetailAsync(request.ExpenseRequestId);
    }
}

