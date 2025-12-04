using ExpenseApprovalSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExpenseApprovalSystem.Application.Interfaces;

public interface IExpenseApprovalDbContext
{
    DbSet<ExpenseRequest> ExpenseRequests { get; }
    DbSet<User> Users { get; }
    DbSet<Department> Departments { get; }
    DbSet<ApprovalStep> ApprovalSteps { get; }
    DbSet<RequestLog> RequestLogs { get; }
    DbSet<ExpenseAttachment> ExpenseAttachments { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}











