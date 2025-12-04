using ExpenseApprovalSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseApprovalSystem.Domain.Entities
{
    public class ApprovalStep
    {
        public int ApprovalStepID { get; set; }
        public int ExpenseRequestId { get; set; }
        public ExpenseRequest ExpenseRequest{ get; set; }
        public int StepNumber{ get; set; }
        public int ApproverId { get; set; }
        public User Approver { get; set; }

        public ApprovalStepType Type { get; set; } // Manager, Finance, HR, Director

        public ApprovalStatus Status { get; set; } // Pending, Approved, Rejected
        public DateTime? ActionDate { get; set; } //tarih = onaylama veya reddetme 
        public DateTime? DueDate { get; set; } // Son onay tarihi
        public string? Comment{ get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
