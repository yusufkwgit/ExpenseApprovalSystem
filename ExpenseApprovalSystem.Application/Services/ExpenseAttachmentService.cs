using AutoMapper;
using ExpenseApprovalSystem.Application.DTOs;
using ExpenseApprovalSystem.Application.Interfaces;
using ExpenseApprovalSystem.Application.Services.Interfaces;
using ExpenseApprovalSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExpenseApprovalSystem.Application.Services;

public sealed class ExpenseAttachmentService : IExpenseAttachmentService
{
    private readonly IExpenseApprovalDbContext _context;
    private readonly IMapper _mapper;

    public ExpenseAttachmentService(IExpenseApprovalDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<ExpenseAttachmentDTO>> GetByRequestAsync(int expenseRequestId)
    {
        var attachments = await _context.ExpenseAttachments
            .Include(a => a.UploadedByUser)
            .Where(a => a.ExpenseRequestId == expenseRequestId && !a.IsDeleted)
            .OrderByDescending(a => a.UploadedAt)
            .ToListAsync();

        return attachments
            .Select(a => _mapper.Map<ExpenseAttachmentDTO>(a))
            .ToList()
            .AsReadOnly();
    }

    public async Task<ExpenseAttachmentDTO> AddAsync(CreateExpenseAttachmentDTO dto)
    {
        var requestExists = await _context.ExpenseRequests
            .AnyAsync(r => r.ExpenseRequestID == dto.ExpenseRequestId && !r.IsDeleted);

        if (!requestExists)
        {
            throw new InvalidOperationException($"ExpenseRequest {dto.ExpenseRequestId} bulunamadı.");
        }

        var uploader = await _context.Users
            .FirstOrDefaultAsync(u => u.UserID == dto.UploadedBy && u.IsActive);

        if (uploader is null)
        { 
            throw new InvalidOperationException($"User {dto.UploadedBy} bulunamadı.");
        }

        if (dto.File == null || dto.File.Length == 0)
        {
            throw new InvalidOperationException("Yüklenecek dosya bulunamadı.");
        }

        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

        if (!Directory.Exists(uploadsFolder))
        {
            Directory.CreateDirectory(uploadsFolder);
        }

        var uniqueFileName = Guid.NewGuid().ToString() + "_" + dto.File.FileName;
        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await dto.File.CopyToAsync(stream);
        }
        
        var entity = new ExpenseAttachment
        {
            ExpenseRequestId = dto.ExpenseRequestId,
            UploadedBy = dto.UploadedBy,
            UploadedAt = DateTime.UtcNow,
            IsDeleted = false,

            FileName = dto.File.FileName,
            ContentType = dto.File.ContentType ?? "application/octet-stream", 
            FileSize = dto.File.Length,
            FilePath = "/uploads/" + uniqueFileName
        };

        _context.ExpenseAttachments.Add(entity);
        await _context.SaveChangesAsync();

        return _mapper.Map<ExpenseAttachmentDTO>(entity);
    }

    public async Task RemoveAsync(int attachmentId)
    {
        var entity = await _context.ExpenseAttachments
            .FirstOrDefaultAsync(a => a.ExpenseAttachmentID == attachmentId && !a.IsDeleted);

        if (entity is null)
        {
            throw new KeyNotFoundException($"ExpenseAttachment {attachmentId} bulunamadı.");
        }

        //if (entity.UploadedBy != performedBy)
        //{
        //    throw new InvalidOperationException("Bu dosyayı silme yetkiniz yok.");
        //}

        entity.IsDeleted = true;
        entity.DeletedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
    }
}


