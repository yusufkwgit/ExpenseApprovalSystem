using AutoMapper;
using ExpenseApprovalSystem.Application.DTOs;
using ExpenseApprovalSystem.Application.Interfaces;
using ExpenseApprovalSystem.Application.Services.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpenseApprovalSystem.Application.CQRS.ExpenseRequests.Commands.UpdateExpenseRequest;

public sealed record UpdateExpenseRequestCommand(int ExpenseRequestId, UpdateExpenseRequestDTO Dto, int CurrentUserId)
    : IRequest;

public sealed class UpdateExpenseRequestCommandHandler : IRequestHandler<UpdateExpenseRequestCommand>
{
    private readonly IExpenseRequestService _expenseRequestService;

    public UpdateExpenseRequestCommandHandler(IExpenseRequestService expenseRequestService)
    {
        _expenseRequestService = expenseRequestService;
    }

    public async Task Handle(UpdateExpenseRequestCommand request, CancellationToken cancellationToken)
    {
        await _expenseRequestService.UpdateAsync(request.ExpenseRequestId, request.Dto, request.CurrentUserId);
    }
}

