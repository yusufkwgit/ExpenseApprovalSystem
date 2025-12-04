using ExpenseApprovalSystem.Application.DTOs;

namespace ExpenseApprovalSystem.Application.Services.Interfaces;

public interface IRequestLogService
{
    Task<IReadOnlyList<RequestLogDTO>> GetByRequestAsync(int expenseRequestId);
    Task<RequestLogDTO> AddLogAsync(CreateRequestLogDTO dto);
}

