using AutoMapper;
using ExpenseApprovalSystem.Application.DTOs;
using ExpenseApprovalSystem.Application.Interfaces;
using ExpenseApprovalSystem.Application.Services.Interfaces;
using ExpenseApprovalSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExpenseApprovalSystem.Application.Services;

public sealed class DepartmentService : IDepartmentService
{
    private readonly IExpenseApprovalDbContext _context;
    private readonly IMapper _mapper;

    public DepartmentService(IExpenseApprovalDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<DepartmentDTO>> GetAllAsync()
    {
        var departments = await _context.Departments  
            .OrderBy(d => d.Name)
            .ToListAsync();

        return departments
            .Select(d => _mapper.Map<DepartmentDTO>(d))
            .ToList()
            .AsReadOnly();
    }

    public async Task<DepartmentDTO?> GetByIdAsync(int departmentId)
    {
        var department = await _context.Departments
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.DepartmentID == departmentId);

        return department is null ? null : _mapper.Map<DepartmentDTO>(department);
    }

    public async Task<DepartmentDTO> CreateAsync(CreateDepartmentDTO dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        var entity = _mapper.Map<Department>(dto);
        entity.CreatedAt = DateTime.UtcNow;
        entity.IsActive = true;

        _context.Departments.Add(entity);
        await _context.SaveChangesAsync();

        return _mapper.Map<DepartmentDTO>(entity);
    }

    public async Task UpdateAsync(int departmentId, UpdateDepartmentDTO dto)
    {
        var entity = await _context.Departments.FirstOrDefaultAsync(d => d.DepartmentID == departmentId);

        if (entity is null)
        {
            throw new KeyNotFoundException($"Department {departmentId} bulunamadı.");
        }

        _mapper.Map(dto, entity);

        await _context.SaveChangesAsync();
    }

    public async Task StatusDepartment (int departmentId)
    {
        var entity = await _context.Departments.FirstOrDefaultAsync(d => d.DepartmentID == departmentId);

        if (entity is null)
        {
            throw new KeyNotFoundException($"Department {departmentId} bulunamadı.");
        }

        entity.IsActive = !entity.IsActive;

        await _context.SaveChangesAsync();
    }

   
}











