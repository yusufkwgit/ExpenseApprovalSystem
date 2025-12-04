using ExpenseApprovalSystem.Application.DTOs;

namespace ExpenseApprovalSystem.Application.Services.Interfaces;

public interface IExpenseRequestService
{
    Task<IReadOnlyList<ExpenseRequestListDTO>> GetAllAsync();
    Task<IReadOnlyList<ExpenseRequestListDTO>> GetByEmployeeAsync(int employeeId);
    Task<ExpenseRequestDTO?> GetDetailAsync(int expenseRequestId);
    Task<ExpenseRequestDTO> CreateAsync(CreateExpenseRequestDTO dto, int currentUserId);
    Task UpdateAsync(int expenseRequestId, UpdateExpenseRequestDTO dto, int currentUserId);
    Task SoftDeleteAsync(int expenseRequestId, int currentUserId);
}



