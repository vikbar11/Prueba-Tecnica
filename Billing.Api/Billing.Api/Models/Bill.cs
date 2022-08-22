using System;
using System.Collections.Generic;

namespace Billing.Api.Models
{
    public partial class Bill
    {
        public Bill()
        {
            BillDetails = new HashSet<BillDetail>();
        }

        public int IdBill { get; set; }        
        public DateTime BillDate { get; set; }
        public int IdCustomer { get; set; }

        public virtual Customer IdCustomerNavigation { get; set; } = null!;
        public virtual ICollection<BillDetail> BillDetails { get; set; }
    }
}
