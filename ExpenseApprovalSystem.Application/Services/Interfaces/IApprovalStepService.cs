using ExpenseApprovalSystem.Application.DTOs;

namespace ExpenseApprovalSystem.Application.Services.Interfaces;

public interface IApprovalStepService
{
    Task<IReadOnlyList<ApprovalStepDTO>> GetStepsForRequestAsync(int expenseRequestId);
    Task<ApprovalStepDTO> AddStepAsync(int expenseRequestId, CreateApprovalStepDTO dto);
    Task UpdateStepAsync(int approvalStepId, UpdateApprovalStepDTO dto);
    Task UpdateStepByRequestAsync(int expenseRequestId, UpdateApprovalStepDTO dto, int currentUserId);
}



