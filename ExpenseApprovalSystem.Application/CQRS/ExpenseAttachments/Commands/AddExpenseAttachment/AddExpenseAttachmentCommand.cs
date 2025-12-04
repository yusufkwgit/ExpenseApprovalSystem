using AutoMapper;
using ExpenseApprovalSystem.Application.DTOs;
using ExpenseApprovalSystem.Application.Interfaces;
using ExpenseApprovalSystem.Application.Services.Interfaces;
using ExpenseApprovalSystem.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpenseApprovalSystem.Application.CQRS.ExpenseAttachments.Commands.AddExpenseAttachment;

public sealed record AddExpenseAttachmentCommand(int ExpenseRequestId, CreateExpenseAttachmentDTO Dto)
    : IRequest<ExpenseAttachmentDTO>;

public sealed class AddExpenseAttachmentCommandHandler
    : IRequestHandler<AddExpenseAttachmentCommand, ExpenseAttachmentDTO>
{
    private readonly IExpenseAttachmentService _expenseAttachmentService;

    public AddExpenseAttachmentCommandHandler( IExpenseAttachmentService expenseAttachmentService )
    {
        _expenseAttachmentService = expenseAttachmentService;
    }

    public async Task<ExpenseAttachmentDTO> Handle(AddExpenseAttachmentCommand request, CancellationToken cancellationToken)
    {
        return await _expenseAttachmentService.AddAsync(request.Dto);
    }
}





