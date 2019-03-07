using System;
using System.Collections.Generic;

namespace Project0.DataAccess
{
    public partial class CupcakeOrderItem
    {
        public int CupcakeOrderItemId { get; set; }
        public int OrderId { get; set; }
        public int CupcakeId { get; set; }
        public int Quantity { get; set; }

        public virtual Cupcake Cupcake { get; set; }
        public virtual CupcakeOrder Order { get; set; }
    }
}
