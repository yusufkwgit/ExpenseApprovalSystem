using AutoMapper;
using ExpenseApprovalSystem.Application.DTOs;
using ExpenseApprovalSystem.Application.Interfaces;
using ExpenseApprovalSystem.Application.Services.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpenseApprovalSystem.Application.CQRS.ExpenseAttachments.Queries.GetExpenseAttachmentsByRequest;

public sealed record GetExpenseAttachmentsByRequestQuery(int ExpenseRequestId)
    : IRequest<IReadOnlyList<ExpenseAttachmentDTO>>;

public sealed class GetExpenseAttachmentsByRequestQueryHandler
    : IRequestHandler<GetExpenseAttachmentsByRequestQuery, IReadOnlyList<ExpenseAttachmentDTO>>
{
    private readonly IExpenseAttachmentService _expenseAttachmentService;

    public GetExpenseAttachmentsByRequestQueryHandler(IExpenseAttachmentService expenseAttachmentService)
    {
        _expenseAttachmentService = expenseAttachmentService;
    }

    public async Task<IReadOnlyList<ExpenseAttachmentDTO>> Handle(
        GetExpenseAttachmentsByRequestQuery request,
        CancellationToken cancellationToken)
    {
        return await _expenseAttachmentService.GetByRequestAsync(request.ExpenseRequestId);
    }
}





