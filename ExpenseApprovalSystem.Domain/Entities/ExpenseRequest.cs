using ExpenseApprovalSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseApprovalSystem.Domain.Entities
{
    public class ExpenseRequest
    {
        public int ExpenseRequestID { get; set; }
        public int EmployeeId { get; set; } 
        public User Employee { get; set; }

        public decimal Amount { get; set; } 
        public string Currency { get; set; } //birim
        public string Category { get; set; } 
        public string Description { get; set; } 
        public DateTime ExpenseDate { get; set; }

        public RequestStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }

        public List<ApprovalStep> ApprovalSteps { get; set; } = new List<ApprovalStep>();
        public List<RequestLog> RequestLogs { get; set; } = new List<RequestLog>();
        public List<ExpenseAttachment> ExpenseAttachments { get; set; } = new List<ExpenseAttachment>();
    }
}
