using System;
using System.Collections.Generic;

namespace Billing.Api.Models
{
    public partial class BillDetail
    {
        public int IdBillDetail { get; set; }
        public string? Description { get; set; }
        public int IdBill { get; set; }
        public int IdProduct { get; set; }
        public int Quantity { get; set; }

        public virtual Bill IdBillNavigation { get; set; } = null!;
        public virtual Product IdProductNavigation { get; set; } = null!;
    }
}
