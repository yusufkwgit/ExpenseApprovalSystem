using ExpenseApprovalSystem.Application.DTOs;

namespace ExpenseApprovalSystem.Application.Services.Interfaces;

public interface IExpenseAttachmentService
{
    Task<IReadOnlyList<ExpenseAttachmentDTO>> GetByRequestAsync(int expenseRequestId);
    Task<ExpenseAttachmentDTO> AddAsync(CreateExpenseAttachmentDTO dto);
    Task RemoveAsync(int attachmentId);
}



