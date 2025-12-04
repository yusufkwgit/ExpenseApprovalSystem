using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ExpenseApprovalSystem.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "DepartmentID", "Code", "CreatedAt", "IsActive", "Name", "ParentDepartmentId" },
                values: new object[,]
                {
                    { 1, "FIN", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, "Finance", null },
                    { 2, "HR", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, "Human Resources", null },
                    { 3, "ENG", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, "Engineering", null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "CreatedAt", "DepartmentId", "Email", "IsActive", "NameSurname", "PasswordHash", "Role", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, "alice@company.com", true, "Alice Johnson", "demo", 0, null },
                    { 2, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, "brian@company.com", true, "Brian Smith", "demo", 1, null },
                    { 3, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, "catherine@company.com", true, "Catherine Lee", "demo", 1, null },
                    { 4, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, "david@company.com", true, "David Chen", "demo", 2, null },
                    { 5, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, "emma@company.com", true, "Emma Davis", "demo", 3, null }
                });

            migrationBuilder.InsertData(
                table: "ExpenseRequests",
                columns: new[] { "ExpenseRequestID", "Amount", "Category", "CreatedAt", "CreatedBy", "Currency", "DeletedAt", "Description", "EmployeeId", "ExpenseDate", "IsDeleted", "Status", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, 250.00m, "Travel", new DateTime(2024, 3, 15, 0, 0, 0, 0, DateTimeKind.Utc), 1, "USD", null, "Client visit flight tickets", 1, new DateTime(2024, 3, 15, 0, 0, 0, 0, DateTimeKind.Utc), false, 0, null, null },
                    { 2, 120.50m, "Meals", new DateTime(2024, 4, 10, 0, 0, 0, 0, DateTimeKind.Utc), 1, "USD", null, "Team lunch with partners", 1, new DateTime(2024, 4, 10, 0, 0, 0, 0, DateTimeKind.Utc), false, 4, new DateTime(2024, 4, 11, 0, 0, 0, 0, DateTimeKind.Utc), 2 },
                    { 3, 980.00m, "Equipment", new DateTime(2024, 5, 5, 0, 0, 0, 0, DateTimeKind.Utc), 2, "USD", null, "New developer laptop", 2, new DateTime(2024, 5, 5, 0, 0, 0, 0, DateTimeKind.Utc), false, 5, new DateTime(2024, 5, 7, 0, 0, 0, 0, DateTimeKind.Utc), 4 }
                });

            migrationBuilder.InsertData(
                table: "ApprovalSteps",
                columns: new[] { "ApprovalStepID", "ActionDate", "ApproverId", "Comment", "CreatedAt", "DueDate", "ExpenseRequestId", "Status", "StepNumber", "Type" },
                values: new object[,]
                {
                    { 1, null, 2, null, new DateTime(2024, 3, 15, 0, 0, 0, 0, DateTimeKind.Utc), null, 1, 0, 1, 0 },
                    { 2, null, 4, null, new DateTime(2024, 3, 15, 0, 0, 0, 0, DateTimeKind.Utc), null, 1, 0, 2, 1 },
                    { 3, new DateTime(2024, 4, 11, 0, 0, 0, 0, DateTimeKind.Utc), 2, null, new DateTime(2024, 4, 10, 0, 0, 0, 0, DateTimeKind.Utc), null, 2, 1, 1, 0 },
                    { 4, new DateTime(2024, 4, 11, 0, 0, 0, 0, DateTimeKind.Utc), 4, null, new DateTime(2024, 4, 10, 0, 0, 0, 0, DateTimeKind.Utc), null, 2, 1, 2, 1 },
                    { 5, new DateTime(2024, 5, 7, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Budget exceeded", new DateTime(2024, 5, 5, 0, 0, 0, 0, DateTimeKind.Utc), null, 3, 2, 1, 1 }
                });

            migrationBuilder.InsertData(
                table: "ExpenseAttachments",
                columns: new[] { "ExpenseAttachmentID", "ContentType", "DeletedAt", "ExpenseRequestId", "FileName", "FilePath", "FileSize", "IsDeleted", "UploadedAt", "UploadedBy" },
                values: new object[,]
                {
                    { 1, "application/pdf", null, 1, "flight.pdf", "/files/receipts/flight.pdf", 245678L, false, new DateTime(2024, 3, 15, 0, 0, 0, 0, DateTimeKind.Utc), 1 },
                    { 2, "application/pdf", null, 1, "hotel.pdf", "/files/receipts/hotel.pdf", 156789L, false, new DateTime(2024, 3, 16, 0, 0, 0, 0, DateTimeKind.Utc), 1 },
                    { 3, "image/jpeg", null, 2, "lunch.jpg", "/files/receipts/lunch.jpg", 56789L, false, new DateTime(2024, 4, 10, 0, 0, 0, 0, DateTimeKind.Utc), 1 }
                });

            migrationBuilder.InsertData(
                table: "RequestLogs",
                columns: new[] { "RequestLogID", "ActionBy", "ActionDate", "ActionType", "Description", "ExpenseRequestId", "IpAddress", "NewValue", "OldValue", "UserId" },
                values: new object[,]
                {
                    { 1, "Alice Johnson", new DateTime(2024, 3, 15, 0, 0, 0, 0, DateTimeKind.Utc), 0, "Request submitted", 1, null, "Pending", null, 1 },
                    { 2, "Alice Johnson", new DateTime(2024, 4, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, "Request submitted", 2, null, "Pending", null, 1 },
                    { 3, "Brian Smith", new DateTime(2024, 4, 11, 0, 0, 0, 0, DateTimeKind.Utc), 2, "Manager approval", 2, null, "Approved", "Pending", 2 },
                    { 4, "Brian Smith", new DateTime(2024, 5, 5, 0, 0, 0, 0, DateTimeKind.Utc), 0, "Request submitted", 3, null, "Pending", null, 2 },
                    { 5, "David Chen", new DateTime(2024, 5, 7, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Cap exceeded", 3, null, "Rejected", "Pending", 4 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ApprovalSteps",
                keyColumn: "ApprovalStepID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ApprovalSteps",
                keyColumn: "ApprovalStepID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ApprovalSteps",
                keyColumn: "ApprovalStepID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ApprovalSteps",
                keyColumn: "ApprovalStepID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ApprovalSteps",
                keyColumn: "ApprovalStepID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ExpenseAttachments",
                keyColumn: "ExpenseAttachmentID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ExpenseAttachments",
                keyColumn: "ExpenseAttachmentID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ExpenseAttachments",
                keyColumn: "ExpenseAttachmentID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "RequestLogs",
                keyColumn: "RequestLogID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "RequestLogs",
                keyColumn: "RequestLogID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "RequestLogs",
                keyColumn: "RequestLogID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "RequestLogs",
                keyColumn: "RequestLogID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "RequestLogs",
                keyColumn: "RequestLogID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "DepartmentID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ExpenseRequests",
                keyColumn: "ExpenseRequestID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ExpenseRequests",
                keyColumn: "ExpenseRequestID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ExpenseRequests",
                keyColumn: "ExpenseRequestID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "DepartmentID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "DepartmentID",
                keyValue: 3);
        }
    }
}
