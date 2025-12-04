using AutoMapper;
using ExpenseApprovalSystem.Application.DTOs;
using ExpenseApprovalSystem.Application.Mappings;
using ExpenseApprovalSystem.Application.Services;
using ExpenseApprovalSystem.Domain.Entities;
using ExpenseApprovalSystem.Domain.Enums;
using ExpenseApprovalSystem.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;

namespace ExpenseApprovalSystem.IntegrationTests;

public class ApprovalStepIntegrationTests
{
    private const string ConnectionString = "Server=DESKTOP-IGOSQTA\\SQLEXPRESS01;Database=ExpenseApprovalDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;";

    [Fact]
    public async Task UpdateStepAsync_WhenFirstStepRejected_MarksRequestAsRejected()
    {
        var uniqueKey = Guid.NewGuid().ToString("N");

        await using var arrangeContext = CreateDbContext();
        var service = new ApprovalStepService(arrangeContext, CreateMapper());

        var seeded = await SeedRequestWithTwoStepsAsync(arrangeContext, uniqueKey);

        var rejectDto = new UpdateApprovalStepDTO
        {
            Status = ApprovalStatus.Rejected,
            Comment = "Integration test rejection"
        };

        await service.UpdateStepAsync(seeded.FirstStepId, rejectDto);

        await using var assertContext = CreateDbContext();
        var request = await assertContext.ExpenseRequests
            .AsNoTracking()
            .FirstAsync(er => er.ExpenseRequestID == seeded.RequestId);

        Assert.Equal(RequestStatus.Rejected, request.Status);
    }

    [Fact]
    public async Task UpdateStepAsync_WhenAllStepsApproved_MarksRequestAsApproved()
    {
        var uniqueKey = Guid.NewGuid().ToString("N");

        await using var arrangeContext = CreateDbContext();
        var service = new ApprovalStepService(arrangeContext, CreateMapper());

        var seeded = await SeedRequestWithTwoStepsAsync(arrangeContext, uniqueKey);

        var approveDto = new UpdateApprovalStepDTO
        {
            Status = ApprovalStatus.Approved,
            Comment = "Approved by integration test"
        };

        await service.UpdateStepAsync(seeded.FirstStepId, approveDto);

        var secondApproveDto = new UpdateApprovalStepDTO
        {
            Status = ApprovalStatus.Approved,
            Comment = "Second approval"
        };

        await service.UpdateStepAsync(seeded.SecondStepId, secondApproveDto);

        await using var assertContext = CreateDbContext();
        var request = await assertContext.ExpenseRequests
            .AsNoTracking()
            .FirstAsync(er => er.ExpenseRequestID == seeded.RequestId);

        Assert.Equal(RequestStatus.Approved, request.Status);
    }

    private static ExpenseApprovalDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<ExpenseApprovalDbContext>()
            .UseSqlServer(ConnectionString)
            .EnableSensitiveDataLogging()
            .Options;

        return new ExpenseApprovalDbContext(options);
    }

    private static IMapper CreateMapper()
    {
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
        return configuration.CreateMapper();
    }

    private static async Task<SeededRequestData> SeedRequestWithTwoStepsAsync(
        ExpenseApprovalDbContext context,
        string uniqueKey)
    {
        var now = DateTime.UtcNow;
        var department = new Department
        {
            Name = $"Integration Dept {uniqueKey[..8]}",
            Code = $"INT-{uniqueKey[..6]}",
            IsActive = true,
            CreatedAt = now
        };

        context.Departments.Add(department);
        await context.SaveChangesAsync();

        var employee = CreateUser($"employee_{uniqueKey}@tests.com", $"Employee {uniqueKey}", UserRole.Employee, department.DepartmentID, now);
        var approverManager = CreateUser($"manager_{uniqueKey}@tests.com", $"Manager {uniqueKey}", UserRole.Manager, department.DepartmentID, now);
        var approverFinance = CreateUser($"finance_{uniqueKey}@tests.com", $"Finance {uniqueKey}", UserRole.Finance, department.DepartmentID, now);

        context.Users.AddRange(employee, approverManager, approverFinance);
        await context.SaveChangesAsync();

        var request = new ExpenseRequest
        {
            EmployeeId = employee.UserID,
            Employee = employee,
            Amount = 123.45m,
            Currency = "USD",
            Category = "Integration",
            Description = $"Integration request {uniqueKey}",
            ExpenseDate = now,
            Status = RequestStatus.Pending,
            CreatedAt = now,
            CreatedBy = employee.UserID,
            IsDeleted = false
        };

        context.ExpenseRequests.Add(request);
        await context.SaveChangesAsync();

        var step1 = new ApprovalStep
        {
            ExpenseRequestId = request.ExpenseRequestID,
            ExpenseRequest = request,
            StepNumber = 1,
            ApproverId = approverManager.UserID,
            Approver = approverManager,
            Type = ApprovalStepType.Manager,
            Status = ApprovalStatus.Pending,
            CreatedAt = now
        };

        var step2 = new ApprovalStep
        {
            ExpenseRequestId = request.ExpenseRequestID,
            ExpenseRequest = request,
            StepNumber = 2,
            ApproverId = approverFinance.UserID,
            Approver = approverFinance,
            Type = ApprovalStepType.Finance,
            Status = ApprovalStatus.Pending,
            CreatedAt = now
        };

        context.ApprovalSteps.AddRange(step1, step2);
        await context.SaveChangesAsync();

        return new SeededRequestData(request.ExpenseRequestID, step1.ApprovalStepID, step2.ApprovalStepID);
    }

    private static User CreateUser(string email, string name, UserRole role, int departmentId, DateTime createdAt)
        => new()
        {
            NameSurname = name,
            Email = email,
            PasswordHash = "integration-test",
            Role = role,
            DepartmentId = departmentId,
            CreatedAt = createdAt,
            IsActive = true
        };

    private sealed record SeededRequestData(int RequestId, int FirstStepId, int SecondStepId);
}

