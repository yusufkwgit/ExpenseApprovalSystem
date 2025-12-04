using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseApprovalSystem.Domain.Enums
{
    public enum RequestStatus
    {
        Pending = 0,            // beklemede - taslak.
        InProgress = 1,         // onay sürecinde
        Approved = 2,           // onaylandi
        Rejected = 3,           // redddedili
        Cancelled = 4           // iptal edildi
    }
}