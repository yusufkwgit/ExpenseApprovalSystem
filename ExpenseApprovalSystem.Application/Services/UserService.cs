using AutoMapper;
using ExpenseApprovalSystem.Application.DTOs;
using ExpenseApprovalSystem.Application.Interfaces;
using ExpenseApprovalSystem.Application.Services.Interfaces;
using ExpenseApprovalSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExpenseApprovalSystem.Application.Services;

public sealed class UserService : IUserService
{
    private readonly IExpenseApprovalDbContext _context;
    private readonly IMapper _mapper;

    public UserService(IExpenseApprovalDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<IReadOnlyList<UserDTO>> GetAllAsync()
    {
        var users = await _context.Users
            .Include(u => u.Department)
            .OrderBy(u => u.UserID)
            .ToListAsync();

        return users
            .Select(u => _mapper.Map<UserDTO>(u))
            .ToList();
    }

    public async Task<UserDTO?> GetByIdAsync(int userId)
    {
        var user = await _context.Users
            .AsNoTracking()
            .Include(u => u.Department)
            .FirstOrDefaultAsync(u => u.UserID == userId);

        return user is null ? null : _mapper.Map<UserDTO>(user);
    }

    public async Task<UserDTO> CreateAsync(CreateUserDTO dto)
    {
        var department = await _context.Departments
            .FirstOrDefaultAsync(d => d.DepartmentID == dto.DepartmentId && d.IsActive);

        if (department is null)
        {
            throw new InvalidOperationException($"Department {dto.DepartmentId} bulunamadı.");
        }

        var entity = new User
        {
            NameSurname = dto.NameSurname,
            Email = dto.Email,
            PasswordHash = dto.Password,
            Role = dto.Role,
            DepartmentId = department.DepartmentID,
            Department = department,
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };

        _context.Users.Add(entity);
        await _context.SaveChangesAsync();

        return _mapper.Map<UserDTO>(entity);
    }

    public async Task UserStatus(int userId)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.UserID == userId);

        if (user is null)
        {
            throw new KeyNotFoundException($"User {userId} bulunamadı.");
        }

        user.IsActive = !user.IsActive;
        user.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
    }


}











