using System;
using System.Collections.Generic;

namespace Billing.Api.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Bills = new HashSet<Bill>();
        }

        public int IdCustomer { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime BornDay { get; set; }

        public virtual ICollection<Bill> Bills { get; set; }
    }
}
