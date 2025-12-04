using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ExpenseApprovalSystem.Infrastructure.EF;

public class ExpenseApprovalDbContextFactory : IDesignTimeDbContextFactory<ExpenseApprovalDbContext>
{
    public ExpenseApprovalDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ExpenseApprovalDbContext>();
        var connectionString = "Server=DESKTOP-IGOSQTA\\SQLEXPRESS01;Database=ExpenseApprovalDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;";
        optionsBuilder.UseSqlServer(connectionString);

        return new ExpenseApprovalDbContext(optionsBuilder.Options);
    }
}

