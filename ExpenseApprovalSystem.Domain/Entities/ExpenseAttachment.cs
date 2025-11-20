using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseApprovalSystem.Domain.Entities
{
    public class ExpenseAttachment
    {
        public int ExpenseAttachmentID { get; set; }

        public int ExpenseRequestId { get; set; } 
        public ExpenseRequest ExpenseRequest { get; set; }

        public string FilePath { get; set; } // DosyaYolu
        public string FileName { get; set; } // DosyaAdI
        public DateTime UploadedAt { get; set; } // YuklenmeTarihi


    }
}
