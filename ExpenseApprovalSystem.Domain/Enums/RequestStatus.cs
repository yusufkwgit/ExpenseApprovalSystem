using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseApprovalSystem.Domain.Enums
{
    public enum RequestStatus
    {
        Pending = 0,            // beklemede
        ManagerApproval = 1,    // yonetici onayi
        FinanceApproval = 2,    // birim onayi
        Approved = 3,           // onaylandi
        Rejected = 4,           // redddedili
        Cancelled = 5           // iptal edildi
    }
}
