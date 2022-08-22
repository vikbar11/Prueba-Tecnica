using System;
using System.Collections.Generic;

namespace Billing.Api.Models
{
    public partial class Product
    {
        public Product()
        {
            BillDetails = new HashSet<BillDetail>();
            Inventories = new HashSet<Inventory>();
        }

        public int IdProduct { get; set; }
        public string ProductName { get; set; } = null!;
        public decimal Price { get; set; }

        public virtual ICollection<BillDetail> BillDetails { get; set; }
        public virtual ICollection<Inventory> Inventories { get; set; }
    }
}
