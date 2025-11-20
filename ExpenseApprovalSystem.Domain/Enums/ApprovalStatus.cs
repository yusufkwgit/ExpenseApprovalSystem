using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseApprovalSystem.Domain.Enums
{
    public enum ApprovalStatus
    {
        Pending = 0,        // beklemede
        Approved = 1,       // onaylandi
        Rejected = 2        // reddedildi
    }
}
