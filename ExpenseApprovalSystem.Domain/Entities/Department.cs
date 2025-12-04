using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseApprovalSystem.Domain.Entities
{
    public class Department
    {
        public int DepartmentID { get; set; }
        public string Name { get; set; }
        public string? Code { get; set; }
        public int? ParentDepartmentId { get; set; } // üst departman
       
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; }
        
        public List<User> Users { get; set; } = new List<User>();
        public List<Department> SubDepartments { get; set; } = new List<Department>();
    }
}
