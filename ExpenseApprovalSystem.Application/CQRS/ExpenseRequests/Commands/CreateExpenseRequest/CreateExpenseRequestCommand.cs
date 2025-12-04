using AutoMapper;
using ExpenseApprovalSystem.Application.DTOs;
using ExpenseApprovalSystem.Application.Interfaces;
using ExpenseApprovalSystem.Application.Services.Interfaces;
using ExpenseApprovalSystem.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpenseApprovalSystem.Application.CQRS.ExpenseRequests.Commands.CreateExpenseRequest;

public sealed record CreateExpenseRequestCommand(CreateExpenseRequestDTO Dto, int CurrentUserId)
    : IRequest<ExpenseRequestDTO>;

public sealed class CreateExpenseRequestCommandHandler
    : IRequestHandler<CreateExpenseRequestCommand, ExpenseRequestDTO>
{
    private readonly IExpenseRequestService _expenseRequestService;

    public CreateExpenseRequestCommandHandler( IExpenseRequestService expenseRequestService)
    {
        _expenseRequestService = expenseRequestService;
    }

    public async Task<ExpenseRequestDTO> Handle(CreateExpenseRequestCommand request, CancellationToken cancellationToken)
    {
        return await _expenseRequestService.CreateAsync(request.Dto, request.CurrentUserId);
    }
}
