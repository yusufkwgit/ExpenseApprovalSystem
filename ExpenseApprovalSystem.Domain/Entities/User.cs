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
        public string Role { get; set; } 

        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public DateTime CreatedAt { get; set; }

        public List<ExpenseRequest> ExpenseRequests { get; set; } // GiderTalepleri
        public List<ApprovalStep> ApprovalSteps { get; set; } // OnayAdimlari (Onaycı olduğu adımlar)


    }
}
