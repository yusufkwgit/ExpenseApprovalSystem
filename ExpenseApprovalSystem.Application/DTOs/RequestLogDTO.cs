using ExpenseApprovalSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseApprovalSystem.Application.DTOs
{
    public class RequestLogDTO
    {
        public int RequestLogID { get; set; }
        public int ExpenseRequestId { get; set; }

        public int UserId { get; set; }
        public string UserName { get; set; }

        public DateTime ActionDate { get; set; }
        public ActionType ActionType { get; set; }
        public string Description { get; set; }

    }
    public class CreateRequestLogDTO
    {
        public int ExpenseRequestId { get; set; }
        public int UserId { get; set; }
        public ActionType ActionType { get; set; }
        public string Description { get; set; }

    }
}

