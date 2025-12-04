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
        public long FileSize { get; set; } // Dosya boyutu
        public string ContentType { get; set; } // type ör: application/pdf, image/jpeg
        public int UploadedBy { get; set; } // Yükleyen kullanıcı ID
        public User? UploadedByUser { get; set; } 
        public DateTime UploadedAt { get; set; } // YuklenmeTarihi
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }
    }
}
