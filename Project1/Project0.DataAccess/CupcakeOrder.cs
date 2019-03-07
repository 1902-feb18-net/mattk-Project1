using System;
using System.Collections.Generic;

namespace Project0.DataAccess
{
    public partial class CupcakeOrder
    {
        public CupcakeOrder()
        {
            CupcakeOrderItem = new HashSet<CupcakeOrderItem>();
        }

        public int OrderId { get; set; }
        public int LocationId { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderTime { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Location Location { get; set; }
        public virtual ICollection<CupcakeOrderItem> CupcakeOrderItem { get; set; }
    }
}
