using MediatR;
using ExpenseApprovalSystem.Application.DTOs;
using ExpenseApprovalSystem.Application.Services.Interfaces;

namespace ExpenseApprovalSystem.Application.CQRS.RequestLogs.Queries;

public sealed record GetExpenseRequestLogQuery(int ExpenseRequestId) : IRequest<IReadOnlyList<RequestLogDTO>>;

public sealed class GetExpenseRequestLogQueryHandler
    : IRequestHandler<GetExpenseRequestLogQuery, IReadOnlyList<RequestLogDTO>>
{
    private readonly IRequestLogService _requestLogService;
    public GetExpenseRequestLogQueryHandler(IRequestLogService requestLogService)
    {
        _requestLogService = requestLogService;
    }
    public async Task<IReadOnlyList<RequestLogDTO>> Handle(GetExpenseRequestLogQuery request, CancellationToken cancellationToken)
    {
        return await _requestLogService.GetByRequestAsync(request.ExpenseRequestId);
    }
}