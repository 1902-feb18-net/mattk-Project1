using System;
using System.Collections.Generic;

namespace Project0.DataAccess
{
    public partial class Cupcake
    {
        public Cupcake()
        {
            CupcakeOrderItem = new HashSet<CupcakeOrderItem>();
            RecipeItem = new HashSet<RecipeItem>();
        }

        public int CupcakeId { get; set; }
        public string Type { get; set; }
        public decimal Cost { get; set; }

        public virtual ICollection<CupcakeOrderItem> CupcakeOrderItem { get; set; }
        public virtual ICollection<RecipeItem> RecipeItem { get; set; }
    }
}
