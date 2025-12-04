using ExpenseApprovalSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseApprovalSystem.Domain.Entities
{
    public class User
    {
        public int UserID { get; set; }
        public string NameSurname { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public UserRole Role { get; set; } 

        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; } = true;


        public List<ExpenseRequest> ExpenseRequests { get; set; } = new List<ExpenseRequest>(); // GiderTalepleri
        public List<ApprovalStep> ApprovalSteps { get; set; } = new List<ApprovalStep>(); // OnayAdimlari (Onaycı olduğu adımlar)
        public List<RequestLog> RequestLogs { get; set; } = new List<RequestLog>(); // İşlem logları
    }
}