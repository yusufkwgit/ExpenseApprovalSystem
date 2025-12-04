using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseApprovalSystem.Application.DTOs
{
    public class DepartmentDTO
    {
        public int DepartmentID { get; set; }
        public string Name { get; set; }
        public string? Code { get; set; } // FIN,
        public int? ParentDepartmentId { get; set; }
        public bool IsActive { get; set; }
    }

    public class CreateDepartmentDTO
    {
        public string Name { get; set; }
        public string? Code { get; set; }
        public int? ParentDepartmentId { get; set; }
    }

    public class UpdateDepartmentDTO
    {
        public string Name { get; set; }
        public string? Code { get; set; }
        public int? ParentDepartmentId { get; set; }
    
    }
}
