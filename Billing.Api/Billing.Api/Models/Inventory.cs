using System;
using System.Collections.Generic;

namespace Billing.Api.Models
{
    public partial class Inventory
    {
        public int IdInventory { get; set; }
        public int IdProduct { get; set; }
        public int Quantity { get; set; }

        public virtual Product IdProductNavigation { get; set; } = null!;
    }
}
