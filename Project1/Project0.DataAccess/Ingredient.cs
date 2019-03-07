using System;
using System.Collections.Generic;

namespace Project0.DataAccess
{
    public partial class Ingredient
    {
        public Ingredient()
        {
            LocationInventory = new HashSet<LocationInventory>();
            RecipeItem = new HashSet<RecipeItem>();
        }

        public int IngredientId { get; set; }
        public string Type { get; set; }
        public string Units { get; set; }

        public virtual ICollection<LocationInventory> LocationInventory { get; set; }
        public virtual ICollection<RecipeItem> RecipeItem { get; set; }
    }
}
