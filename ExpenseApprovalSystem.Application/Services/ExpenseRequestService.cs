using AutoMapper;
using ExpenseApprovalSystem.Application.DTOs;
using ExpenseApprovalSystem.Application.Interfaces;
using ExpenseApprovalSystem.Application.Services.Interfaces;
using ExpenseApprovalSystem.Domain.Entities;
using ExpenseApprovalSystem.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace ExpenseApprovalSystem.Application.Services;

public sealed class ExpenseRequestService : IExpenseRequestService
{
    private readonly IExpenseApprovalDbContext _context;
    private readonly IMapper _mapper;

    public ExpenseRequestService(IExpenseApprovalDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<ExpenseRequestListDTO>> GetAllAsync()
    {
        var entities = await _context.ExpenseRequests
            .Include(r => r.Employee)
            .Where(r => !r.IsDeleted)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync();

        return entities
            .Select(e => _mapper.Map<ExpenseRequestListDTO>(e))
            .ToList()
            .AsReadOnly();
    }

    public async Task<IReadOnlyList<ExpenseRequestListDTO>> GetByEmployeeAsync(int employeeId)
    {
        var entities = await _context.ExpenseRequests
            .Include(r => r.Employee)
            .Where(r => !r.IsDeleted && r.EmployeeId == employeeId)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync();

        return entities
            .Select(e => _mapper.Map<ExpenseRequestListDTO>(e))
            .ToList()
            .AsReadOnly();
    }

    public async Task<ExpenseRequestDTO?> GetDetailAsync(int expenseRequestId)
    {
        var entity = await _context.ExpenseRequests
            .Include(r => r.Employee)
            .FirstOrDefaultAsync(r => r.ExpenseRequestID == expenseRequestId && !r.IsDeleted);

        return entity is null ? null : _mapper.Map<ExpenseRequestDTO>(entity);
    }

    public async Task<ExpenseRequestDTO> CreateAsync(CreateExpenseRequestDTO dto, int currentUserId)
    {
        var employee = await _context.Users
            .FirstOrDefaultAsync(u => u.UserID == dto.EmployeeId && u.IsActive);

        if (employee is null)
        {
            throw new InvalidOperationException($"Employee {dto.EmployeeId} bulunamadı.");
        }

        var entity = _mapper.Map<ExpenseRequest>(dto);
        entity.EmployeeId = employee.UserID;
        entity.Employee = employee;
        entity.CreatedBy = currentUserId;

        entity.Status = RequestStatus.InProgress;

        _context.ExpenseRequests.Add(entity);

        int stepCounter = 1; 

        if (employee.Role != UserRole.Manager)
        {
            var manager = await _context.Users
                .FirstOrDefaultAsync(u => u.DepartmentId == employee.DepartmentId
                                       && u.Role == UserRole.Manager
                                       && u.IsActive);

            if (manager != null)
            {
                AddStepToContext(entity, manager.UserID, ApprovalStepType.Manager, stepCounter);
                stepCounter++; 
            }
        }

        var financeUser = await _context.Users
            .FirstOrDefaultAsync(u => u.Role == UserRole.Finance
                                   && u.IsActive
                                   && u.UserID != currentUserId);

        if (financeUser != null)
        {
            AddStepToContext(entity, financeUser.UserID, ApprovalStepType.Finance, stepCounter);
           
        }
        else
        {
             throw new InvalidOperationException(" başka bir finansçı bulunamadı.");
        }

        await _context.SaveChangesAsync();

        return _mapper.Map<ExpenseRequestDTO>(entity);
    }
    private void AddStepToContext(ExpenseRequest request, int approverId, ApprovalStepType type, int stepNum)
    {
        var step = new ApprovalStep
        {
            ExpenseRequest = request,
            ApproverId = approverId,
            Type = type,
            StepNumber = stepNum,
            Status = ApprovalStatus.Pending,
            CreatedAt = DateTime.UtcNow
        };
        _context.ApprovalSteps.Add(step);
    }
    public async Task UpdateAsync(int expenseRequestId, UpdateExpenseRequestDTO dto, int currentUserId)
    {
        ArgumentNullException.ThrowIfNull(dto);

        var entity = await FindExpenseRequestAsync(expenseRequestId);

        _mapper.Map(dto, entity);
        entity.UpdatedAt = DateTime.UtcNow;
        entity.UpdatedBy = currentUserId;

        await _context.SaveChangesAsync();
    }

    public async Task SoftDeleteAsync(int expenseRequestId, int currentUserId)
    {
        var entity = await FindExpenseRequestAsync(expenseRequestId);

        entity.IsDeleted = true;
        entity.DeletedAt = DateTime.UtcNow;
        entity.UpdatedAt = DateTime.UtcNow;
        entity.UpdatedBy = currentUserId;

        await _context.SaveChangesAsync();
    }

    private async Task<ExpenseRequest> FindExpenseRequestAsync(int expenseRequestId)
    {
        var entity = await _context.ExpenseRequests
            .FirstOrDefaultAsync(r => r.ExpenseRequestID == expenseRequestId && !r.IsDeleted);

        return entity
            ?? throw new KeyNotFoundException($"ExpenseRequest {expenseRequestId} bulunamadı.");
    }
}











