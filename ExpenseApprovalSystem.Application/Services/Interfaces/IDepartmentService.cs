using ExpenseApprovalSystem.Application.DTOs;

namespace ExpenseApprovalSystem.Application.Services.Interfaces;

public interface IDepartmentService
{
    Task<IReadOnlyList<DepartmentDTO>> GetAllAsync();
    Task<DepartmentDTO?> GetByIdAsync(int departmentId);
    Task<DepartmentDTO> CreateAsync(CreateDepartmentDTO dto);
    Task UpdateAsync(int departmentId, UpdateDepartmentDTO dto);
    Task StatusDepartment (int departmentId);

}