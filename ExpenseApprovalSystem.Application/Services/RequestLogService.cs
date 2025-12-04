using AutoMapper;
using ExpenseApprovalSystem.Application.DTOs;
using ExpenseApprovalSystem.Application.Interfaces;
using ExpenseApprovalSystem.Application.Services.Interfaces;
using ExpenseApprovalSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExpenseApprovalSystem.Application.Services;

public sealed class RequestLogService : IRequestLogService
{
    private readonly IExpenseApprovalDbContext _context;
    private readonly IMapper _mapper;

    public RequestLogService(IExpenseApprovalDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<RequestLogDTO>> GetByRequestAsync(int expenseRequestId)
    {
        var logs = await _context.RequestLogs
            .AsNoTracking()
            .Include(l => l.User)
            .Where(l => l.ExpenseRequestId == expenseRequestId)
            .OrderByDescending(l => l.ActionDate)
            .ToListAsync();

        return logs
            .Select(l => _mapper.Map<RequestLogDTO>(l))
            .ToList()
            .AsReadOnly();
    }

    public async Task<RequestLogDTO> AddLogAsync(CreateRequestLogDTO dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        var requestExists = await _context.ExpenseRequests
            .AnyAsync(r => r.ExpenseRequestID == dto.ExpenseRequestId);

        if (!requestExists)
        {
            throw new InvalidOperationException($"ExpenseRequest {dto.ExpenseRequestId} bulunamadı.");
        }

        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.UserID == dto.UserId && u.IsActive);

        if (user is null)
        {
            throw new InvalidOperationException($"User {dto.UserId} bulunamadı.");
        }

        var entity = _mapper.Map<RequestLog>(dto);
        entity.User = user;
        entity.UserId = user.UserID;
        entity.ActionBy = user.NameSurname;

        _context.RequestLogs.Add(entity);
        await _context.SaveChangesAsync();

        return _mapper.Map<RequestLogDTO>(entity);
    }
}











