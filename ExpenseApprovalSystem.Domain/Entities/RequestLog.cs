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
        public ExpenseRequest ExpenseRequest { get; set; }

        // (İşlemi yapan)
        public int UserId { get; set; } 
        public User User { get; set; }

       
        public DateTime ActionDate { get; set; } // IsleminTarihi
        public string ActionBy { get; set; } // IslemiYapanKullanici
        public string ActionType { get; set; } // IsleminTuru (Olusturma, Guncelleme, Onaylama, Reddetme)
        public string Description { get; set; } // Aciklama

    }
}
