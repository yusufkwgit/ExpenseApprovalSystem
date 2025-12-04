using ExpenseApprovalSystem.Application.Interfaces;
using ExpenseApprovalSystem.Application.Services.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpenseApprovalSystem.Application.CQRS.ExpenseAttachments.Commands.DeleteExpenseAttachment;

public sealed record DeleteExpenseAttachmentCommand(int ExpenseRequestId, int AttachmentId, int PerformedBy)
    : IRequest;

public sealed class DeleteExpenseAttachmentCommandHandler
    : IRequestHandler<DeleteExpenseAttachmentCommand>
{
    private readonly IExpenseAttachmentService _expenseAttachmentService;

    public DeleteExpenseAttachmentCommandHandler(IExpenseAttachmentService expenseAttachmentService)
    {
        _expenseAttachmentService = expenseAttachmentService;
    }

    public async Task Handle(DeleteExpenseAttachmentCommand request, CancellationToken cancellationToken)
    {
        await _expenseAttachmentService.RemoveAsync(request.AttachmentId);
    }
}





