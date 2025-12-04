using AutoMapper;
using ExpenseApprovalSystem.Application.DTOs;
using ExpenseApprovalSystem.Application.Interfaces;
using ExpenseApprovalSystem.Application.Services.Interfaces;
using ExpenseApprovalSystem.Domain.Entities;
using ExpenseApprovalSystem.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace ExpenseApprovalSystem.Application.Services;

public sealed class ApprovalStepService : IApprovalStepService
{
    private readonly IExpenseApprovalDbContext _context;
    private readonly IMapper _mapper;

    public ApprovalStepService(IExpenseApprovalDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<ApprovalStepDTO>> GetStepsForRequestAsync(int expenseRequestId)
    {
        var steps = await _context.ApprovalSteps
            .Include(s => s.Approver)
            .Where(s => s.ExpenseRequestId == expenseRequestId)
            .OrderBy(s => s.StepNumber)
            .ToListAsync();

        return steps
            .Select(s => _mapper.Map<ApprovalStepDTO>(s))
            .ToList()
            .AsReadOnly();
    }

    public async Task<ApprovalStepDTO> AddStepAsync(int expenseRequestId, CreateApprovalStepDTO dto)
    {
        var request = await _context.ExpenseRequests
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.ExpenseRequestID == expenseRequestId);

        if (request == null || request.IsDeleted)
        {
            throw new InvalidOperationException($"ExpenseRequest {expenseRequestId} bulunamadı veya silinmiş.");
        }

        if (request.Status == RequestStatus.Approved ||
            request.Status == RequestStatus.Rejected ||
            request.Status == RequestStatus.Cancelled)
        {
            throw new InvalidOperationException($"Bu talep tamamlanmış veya iptal edilmiş (Durum: {request.Status}). Yeni onay adımı eklenemez.");
        }

        var approver = await _context.Users
            .FirstOrDefaultAsync(u => u.UserID == dto.ApproverId && u.IsActive);

        if (approver is null)
        {
            throw new InvalidOperationException($"Approver {dto.ApproverId} bulunamadı.");
        }

        var entity = _mapper.Map<ApprovalStep>(dto);
        entity.ExpenseRequestId = expenseRequestId;
        entity.ApproverId = approver.UserID;
        entity.Approver = approver;
        entity.Status = ApprovalStatus.Pending;

        _context.ApprovalSteps.Add(entity);
        await _context.SaveChangesAsync();

        return _mapper.Map<ApprovalStepDTO>(entity);
    }

    public async Task UpdateStepAsync(int approvalStepId, UpdateApprovalStepDTO dto)
    {
        var step = await _context.ApprovalSteps
            .Include(s => s.ExpenseRequest)
            .FirstOrDefaultAsync(s => s.ApprovalStepID == approvalStepId);

        if (step is null)
        {
            throw new KeyNotFoundException($"ApprovalStep {approvalStepId} bulunamadı.");
        }

        if (step.ExpenseRequest.IsDeleted)
        {
            throw new InvalidOperationException($"ExpenseRequest {step.ExpenseRequestId} silinmiş. Approval step güncellenemez.");
        }

        if (dto.Status.HasValue && step.Status != dto.Status.Value)
        {
            step.Status = dto.Status.Value;
            step.ActionDate = DateTime.UtcNow;
            step.Comment = dto.Comment;

            if (step.Status == ApprovalStatus.Rejected)
            {
                step.ExpenseRequest.Status = RequestStatus.Rejected;

            }

            else if (step.Status == ApprovalStatus.Approved)
            {
                var hasPendingSteps = await _context.ApprovalSteps
                    .AnyAsync(s => s.ExpenseRequestId == step.ExpenseRequestId
                                && s.ApprovalStepID != step.ApprovalStepID
                                && s.Status == ApprovalStatus.Pending);

                if (!hasPendingSteps)
                {
                    step.ExpenseRequest.Status = RequestStatus.Approved;
                }
                else
                {
                }
            }
        }
        else if (dto.Comment != null)
        {
            step.Comment = dto.Comment;
        }

        await _context.SaveChangesAsync();
    }


    public async Task UpdateStepByRequestAsync(int expenseRequestId, UpdateApprovalStepDTO dto, int currentUserId)
    {
        var step = await _context.ApprovalSteps
            .Include(s => s.ExpenseRequest)
            .Where(s => s.ExpenseRequestId == expenseRequestId
                        && s.Status != ApprovalStatus.Approved
                        && !s.ExpenseRequest.IsDeleted
                        && s.ApproverId == currentUserId)
            .OrderBy(s => s.StepNumber)
            .FirstOrDefaultAsync();

        if (step is null)
        {
            throw new KeyNotFoundException($"ExpenseRequest {expenseRequestId} için size atanmış güncellenebilir bir adım bulunamadı.");
        }

        if (step.ExpenseRequest.Status == RequestStatus.Rejected ||
            step.ExpenseRequest.Status == RequestStatus.Cancelled ||
            step.ExpenseRequest.Status == RequestStatus.Approved)
        {
            throw new InvalidOperationException($"Bu talep zaten sonuçlanmış (Durum: {step.ExpenseRequest.Status}). Artık işlem yapılamaz.");
        }
        var value = await _context.ApprovalSteps
            .AnyAsync(s => s.ExpenseRequestId == expenseRequestId
                        && s.StepNumber < step.StepNumber 
                        && s.Status != ApprovalStatus.Approved); 

        if (value)
        {
            throw new InvalidOperationException("Yöneticinin onayı bekleniyor.");
        }
        await UpdateStepAsync(step.ApprovalStepID, dto);
    }
}
