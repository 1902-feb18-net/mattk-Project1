using System;
using System.Collections.Generic;

namespace Project1.DataAccess.DataClasses
{
    public partial class Location
    {
        public Location()
        {
            CupcakeOrder = new HashSet<CupcakeOrder>();
            Customer = new HashSet<Customer>();
            LocationInventory = new HashSet<LocationInventory>();
        }

        public int LocationId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CupcakeOrder> CupcakeOrder { get; set; }
        public virtual ICollection<Customer> Customer { get; set; }
        public virtual ICollection<LocationInventory> LocationInventory { get; set; }
    }
}
