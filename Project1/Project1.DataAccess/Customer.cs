using System;
using System.Collections.Generic;

namespace Project1.DataAccess
{
    public partial class Customer
    {
        public Customer()
        {
            CupcakeOrder = new HashSet<CupcakeOrder>();
        }

        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? DefaultLocation { get; set; }

        public virtual Location DefaultLocationNavigation { get; set; }
        public virtual ICollection<CupcakeOrder> CupcakeOrder { get; set; }
    }
}
