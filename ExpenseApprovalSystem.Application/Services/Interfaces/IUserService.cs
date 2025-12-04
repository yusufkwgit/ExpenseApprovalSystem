using ExpenseApprovalSystem.Application.DTOs;

namespace ExpenseApprovalSystem.Application.Services.Interfaces;

public interface IUserService
{
    Task<IReadOnlyList<UserDTO>> GetAllAsync();
    Task<UserDTO?> GetByIdAsync(int userId);
    Task<UserDTO> CreateAsync(CreateUserDTO dto);
    Task UserStatus (int userId);
}



