using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ExpenseApprovalSystem.Application.DTOs
{
    public class ExpenseAttachmentDTO
    {
        public int ExpenseAttachmentID { get; set; }
        public string FileName { get; set; }
        public long FileSize { get; set; }
        public string? ContentType { get; set; }
        public DateTime UploadedAt { get; set; }
        public int UploadedBy { get; set; }
        public string UploadedByName { get; set; }
    }
    public class CreateExpenseAttachmentDTO
    {
            public int ExpenseRequestId { get; set; }
            public IFormFile File { get; set; } 
            public int UploadedBy { get; set; } 
        
    }
}

