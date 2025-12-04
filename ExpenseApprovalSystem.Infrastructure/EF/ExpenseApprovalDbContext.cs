using ExpenseApprovalSystem.Application.Interfaces;
using ExpenseApprovalSystem.Domain.Entities;
using ExpenseApprovalSystem.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace ExpenseApprovalSystem.Infrastructure.EF;

public class ExpenseApprovalDbContext(DbContextOptions<ExpenseApprovalDbContext> options)
    : DbContext(options), IExpenseApprovalDbContext
{
    public DbSet<ExpenseRequest> ExpenseRequests => Set<ExpenseRequest>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<ApprovalStep> ApprovalSteps => Set<ApprovalStep>();
    public DbSet<RequestLog> RequestLogs => Set<RequestLog>();
    public DbSet<ExpenseAttachment> ExpenseAttachments => Set<ExpenseAttachment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ConfigureDepartments(modelBuilder);
        ConfigureUsers(modelBuilder);
        ConfigureExpenseRequests(modelBuilder);
        ConfigureApprovalSteps(modelBuilder);
        ConfigureRequestLogs(modelBuilder);
        ConfigureExpenseAttachments(modelBuilder);
        SeedInitialData(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }

    private static void ConfigureDepartments(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<Department>();

        entity.ToTable("Departments");
        entity.HasKey(d => d.DepartmentID);
        entity.Property(d => d.Name)
            .HasMaxLength(200)
            .IsRequired();
        entity.Property(d => d.Code)
            .HasMaxLength(50);

        entity.HasOne<Department>()
            .WithMany(d => d.SubDepartments)
            .HasForeignKey(d => d.ParentDepartmentId)
            .OnDelete(DeleteBehavior.Restrict);
    }

    private static void ConfigureUsers(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<User>();

        entity.ToTable("Users");
        entity.HasKey(u => u.UserID);
        entity.Property(u => u.NameSurname)
            .HasMaxLength(200)
            .IsRequired();
        entity.Property(u => u.Email)
            .HasMaxLength(320)
            .IsRequired();
        entity.Property(u => u.PasswordHash)
            .HasMaxLength(500)
            .IsRequired();

        entity.HasOne(u => u.Department)
            .WithMany(d => d.Users)
            .HasForeignKey(u => u.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);
    }

    private static void ConfigureExpenseRequests(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<ExpenseRequest>();

        entity.ToTable("ExpenseRequests");
        entity.HasKey(er => er.ExpenseRequestID);

        entity.Property(er => er.Currency)
            .HasMaxLength(10)
            .IsRequired();
        entity.Property(er => er.Category)
            .HasMaxLength(100)
            .IsRequired();
        entity.Property(er => er.Description)
            .HasMaxLength(2000);
        entity.Property(er => er.Amount)
            .HasColumnType("decimal(18,2)");

        entity.HasOne(er => er.Employee)
            .WithMany(u => u.ExpenseRequests)
            .HasForeignKey(er => er.EmployeeId)
            .OnDelete(DeleteBehavior.Restrict);
    }

    private static void ConfigureApprovalSteps(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<ApprovalStep>();

        entity.ToTable("ApprovalSteps");
        entity.HasKey(a => a.ApprovalStepID);

        entity.Property(a => a.Comment)
            .HasMaxLength(1000);

        entity.HasOne(a => a.ExpenseRequest)
            .WithMany(er => er.ApprovalSteps)
            .HasForeignKey(a => a.ExpenseRequestId)
            .OnDelete(DeleteBehavior.Cascade);

        entity.HasOne(a => a.Approver)
            .WithMany(u => u.ApprovalSteps)
            .HasForeignKey(a => a.ApproverId)
            .OnDelete(DeleteBehavior.Restrict);
    }

    private static void ConfigureRequestLogs(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<RequestLog>();

        entity.ToTable("RequestLogs");
        entity.HasKey(rl => rl.RequestLogID);

        entity.Property(rl => rl.Description)
            .HasMaxLength(2000);
        entity.Property(rl => rl.ActionBy)
            .HasMaxLength(200)
            .IsRequired();
        entity.Property(rl => rl.OldValue)
            .HasColumnType("nvarchar(max)");
        entity.Property(rl => rl.NewValue)
            .HasColumnType("nvarchar(max)");

        entity.HasOne(rl => rl.ExpenseRequest)
            .WithMany(er => er.RequestLogs)
            .HasForeignKey(rl => rl.ExpenseRequestId)
            .OnDelete(DeleteBehavior.Cascade);

        entity.HasOne(rl => rl.User)
            .WithMany(u => u.RequestLogs)
            .HasForeignKey(rl => rl.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }

    private static void ConfigureExpenseAttachments(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<ExpenseAttachment>();

        entity.ToTable("ExpenseAttachments");
        entity.HasKey(ea => ea.ExpenseAttachmentID);

        entity.Property(ea => ea.FilePath)
            .HasMaxLength(500)
            .IsRequired();
        entity.Property(ea => ea.FileName)
            .HasMaxLength(255)
            .IsRequired();
        entity.Property(ea => ea.ContentType)
            .HasMaxLength(200)
            .IsRequired();

        entity.HasOne(ea => ea.ExpenseRequest)
            .WithMany(er => er.ExpenseAttachments)
            .HasForeignKey(ea => ea.ExpenseRequestId)
            .OnDelete(DeleteBehavior.Cascade);

        entity.HasOne(ea => ea.UploadedByUser)
            .WithMany()
            .HasForeignKey(ea => ea.UploadedBy)
            .OnDelete(DeleteBehavior.Restrict);
    }

    private static void SeedInitialData(ModelBuilder modelBuilder)
    {
        var createdAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var march = new DateTime(2024, 3, 15, 0, 0, 0, DateTimeKind.Utc);
        var april = new DateTime(2024, 4, 10, 0, 0, 0, DateTimeKind.Utc);
        var may = new DateTime(2024, 5, 5, 0, 0, 0, DateTimeKind.Utc);

        modelBuilder.Entity<Department>().HasData(
            new Department { DepartmentID = 1, Name = "Finance", Code = "FIN", IsActive = true, CreatedAt = createdAt },
            new Department { DepartmentID = 2, Name = "Human Resources", Code = "HR", IsActive = true, CreatedAt = createdAt },
            new Department { DepartmentID = 3, Name = "Engineering", Code = "ENG", IsActive = true, CreatedAt = createdAt }
        );

        modelBuilder.Entity<User>().HasData(
            new User { UserID = 1, NameSurname = "Alice Johnson", Email = "alice@company.com", PasswordHash = "demo", Role = UserRole.Employee, DepartmentId = 3, CreatedAt = createdAt, IsActive = true },
            new User { UserID = 2, NameSurname = "Brian Smith", Email = "brian@company.com", PasswordHash = "demo", Role = UserRole.Manager, DepartmentId = 3, CreatedAt = createdAt, IsActive = true },
            new User { UserID = 3, NameSurname = "Catherine Lee", Email = "catherine@company.com", PasswordHash = "demo", Role = UserRole.Manager, DepartmentId = 2, CreatedAt = createdAt, IsActive = true },
            new User { UserID = 4, NameSurname = "David Chen", Email = "david@company.com", PasswordHash = "demo", Role = UserRole.Finance, DepartmentId = 1, CreatedAt = createdAt, IsActive = true },
            new User { UserID = 5, NameSurname = "Emma Davis", Email = "emma@company.com", PasswordHash = "demo", Role = UserRole.Admin, DepartmentId = 1, CreatedAt = createdAt, IsActive = true }
        );

        modelBuilder.Entity<ExpenseRequest>().HasData(
            new ExpenseRequest { ExpenseRequestID = 1, EmployeeId = 1, Amount = 250.00m, Currency = "USD", Category = "Travel", Description = "Client visit flight tickets", ExpenseDate = march, Status = RequestStatus.Pending, CreatedAt = march, CreatedBy = 1, IsDeleted = false },
            new ExpenseRequest { ExpenseRequestID = 2, EmployeeId = 1, Amount = 120.50m, Currency = "USD", Category = "Meals", Description = "Team lunch with partners", ExpenseDate = april, Status = RequestStatus.Approved, CreatedAt = april, CreatedBy = 1, UpdatedAt = april.AddDays(1), UpdatedBy = 2, IsDeleted = false },
            new ExpenseRequest { ExpenseRequestID = 3, EmployeeId = 2, Amount = 980.00m, Currency = "USD", Category = "Equipment", Description = "New developer laptop", ExpenseDate = may, Status = RequestStatus.Rejected, CreatedAt = may, CreatedBy = 2, UpdatedAt = may.AddDays(2), UpdatedBy = 4, IsDeleted = false }
        );

        modelBuilder.Entity<ApprovalStep>().HasData(
            new ApprovalStep { ApprovalStepID = 1, ExpenseRequestId = 1, StepNumber = 1, ApproverId = 2, Type = ApprovalStepType.Manager, Status = ApprovalStatus.Pending, CreatedAt = march },
            new ApprovalStep { ApprovalStepID = 2, ExpenseRequestId = 1, StepNumber = 2, ApproverId = 4, Type = ApprovalStepType.Finance, Status = ApprovalStatus.Pending, CreatedAt = march },
            new ApprovalStep { ApprovalStepID = 3, ExpenseRequestId = 2, StepNumber = 1, ApproverId = 2, Type = ApprovalStepType.Manager, Status = ApprovalStatus.Approved, ActionDate = april.AddDays(1), CreatedAt = april },
            new ApprovalStep { ApprovalStepID = 4, ExpenseRequestId = 2, StepNumber = 2, ApproverId = 4, Type = ApprovalStepType.Finance, Status = ApprovalStatus.Approved, ActionDate = april.AddDays(1), CreatedAt = april },
            new ApprovalStep { ApprovalStepID = 5, ExpenseRequestId = 3, StepNumber = 1, ApproverId = 4, Type = ApprovalStepType.Finance, Status = ApprovalStatus.Rejected, ActionDate = may.AddDays(2), Comment = "Budget exceeded", CreatedAt = may }
        );

        modelBuilder.Entity<RequestLog>().HasData(
            new RequestLog { RequestLogID = 1, ExpenseRequestId = 1, UserId = 1, ActionDate = march, ActionBy = "Alice Johnson", ActionType = ActionType.Created, Description = "Request submitted", OldValue = null, NewValue = RequestStatus.Pending.ToString() },
            new RequestLog { RequestLogID = 2, ExpenseRequestId = 2, UserId = 1, ActionDate = april, ActionBy = "Alice Johnson", ActionType = ActionType.Created, Description = "Request submitted", NewValue = RequestStatus.Pending.ToString() },
            new RequestLog { RequestLogID = 3, ExpenseRequestId = 2, UserId = 2, ActionDate = april.AddDays(1), ActionBy = "Brian Smith", ActionType = ActionType.Approved, Description = "Manager approval", OldValue = RequestStatus.Pending.ToString(), NewValue = RequestStatus.Approved.ToString() },
            new RequestLog { RequestLogID = 4, ExpenseRequestId = 3, UserId = 2, ActionDate = may, ActionBy = "Brian Smith", ActionType = ActionType.Created, Description = "Request submitted", NewValue = RequestStatus.Pending.ToString() },
            new RequestLog { RequestLogID = 5, ExpenseRequestId = 3, UserId = 4, ActionDate = may.AddDays(2), ActionBy = "David Chen", ActionType = ActionType.Rejected, Description = "Cap exceeded", OldValue = RequestStatus.Pending.ToString(), NewValue = RequestStatus.Rejected.ToString() }
        );

        modelBuilder.Entity<ExpenseAttachment>().HasData(
            new ExpenseAttachment { ExpenseAttachmentID = 1, ExpenseRequestId = 1, FilePath = "/files/receipts/flight.pdf", FileName = "flight.pdf", FileSize = 245678, ContentType = "application/pdf", UploadedBy = 1, UploadedAt = march, IsDeleted = false },
            new ExpenseAttachment { ExpenseAttachmentID = 2, ExpenseRequestId = 1, FilePath = "/files/receipts/hotel.pdf", FileName = "hotel.pdf", FileSize = 156789, ContentType = "application/pdf", UploadedBy = 1, UploadedAt = march.AddDays(1), IsDeleted = false },
            new ExpenseAttachment { ExpenseAttachmentID = 3, ExpenseRequestId = 2, FilePath = "/files/receipts/lunch.jpg", FileName = "lunch.jpg", FileSize = 56789, ContentType = "image/jpeg", UploadedBy = 1, UploadedAt = april, IsDeleted = false }
        );
    }
}

