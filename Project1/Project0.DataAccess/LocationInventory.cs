using System;
using System.Collections.Generic;

namespace Project0.DataAccess
{
    public partial class LocationInventory
    {
        public int LocationInventoryId { get; set; }
        public int LocationId { get; set; }
        public int IngredientId { get; set; }
        public decimal Amount { get; set; }

        public virtual Ingredient Ingredient { get; set; }
        public virtual Location Location { get; set; }
    }
}
