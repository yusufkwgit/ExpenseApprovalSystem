using ExpenseApprovalSystem.Domain.Enums;

namespace ExpenseApprovalSystem.Application.DTOs
{ 
    public class ExpenseRequestDTO
    {
        public int ExpenseRequestID { get; set; }
        public int EmployeeId { get; set; }
        public string? EmployeeName { get; set; } //list sadece isim
        public decimal Amount { get; set; }
        public string Currency { get; set; } //birim
        public string Category { get; set; }
        public string Description { get; set; }
        public DateTime ExpenseDate { get; set; }
        public RequestStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }

    }

    public class CreateExpenseRequestDTO
    {
        public int EmployeeId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public DateTime ExpenseDate { get; set; }
    }

    public class UpdateExpenseRequestDTO
    {
        public decimal? Amount { get; set; }
        public string? Currency { get; set; }
        public string? Category { get; set; }
        public string? Description { get; set; }
        public DateTime? ExpenseDate { get; set; }
    }

    public class ExpenseRequestListDTO
    {
        public int ExpenseRequestID { get; set; }
        public string EmployeeName { get; set; }
        public decimal Amount { get; set; }
        public RequestStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}


