using ExpenseApprovalSystem.Application.Interfaces;
using ExpenseApprovalSystem.Application.Services.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpenseApprovalSystem.Application.CQRS.ExpenseRequests.Commands.SoftDeleteExpenseRequest;

public sealed record SoftDeleteExpenseRequestCommand(int ExpenseRequestId, int CurrentUserId) : IRequest;

public sealed class SoftDeleteExpenseRequestCommandHandler : IRequestHandler<SoftDeleteExpenseRequestCommand>
{
    private readonly IExpenseRequestService _expenseRequestService;

    public SoftDeleteExpenseRequestCommandHandler(IExpenseRequestService expenseRequestService)
    {
        _expenseRequestService = expenseRequestService;
    }

    public async Task Handle(SoftDeleteExpenseRequestCommand request, CancellationToken cancellationToken)
    {
       await _expenseRequestService.SoftDeleteAsync(request.ExpenseRequestId, request.CurrentUserId);
    }
}

