using ExpenseApprovalSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseApprovalSystem.Domain.Entities
{
    public class RequestLog
    {
        public int RequestLogID { get; set; }
        public int ExpenseRequestId { get; set; }
        public ExpenseRequest ExpenseRequest { get; set; } = default!;

        // (İşlemi yapan)
        public int UserId { get; set; } 
        public User User { get; set; } = default!;

        public DateTime ActionDate { get; set; } // IsleminTarihi
        public string ActionBy { get; set; } = string.Empty; // IslemiYapanKullanici
        public ActionType ActionType { get; set; } // IsleminTuru (Olusturma, Guncelleme, Onaylama, Reddetme)
        public string Description { get; set; } = string.Empty; // Aciklama
        public string? OldValue { get; set; } // Önceki değer (JSON formatında)
        public string? NewValue { get; set; } // Yeni değer (JSON formatında)
        public string? IpAddress { get; set; } // İşlem yapılan IP adresi
    }
}
