using System;
using System.Collections.Generic;

namespace Project1.DataAccess.DataClasses
{
    public partial class RecipeItem
    {
        public int RecipeItemId { get; set; }
        public int CupcakeId { get; set; }
        public int IngredientId { get; set; }
        public decimal Amount { get; set; }

        public virtual Cupcake Cupcake { get; set; }
        public virtual Ingredient Ingredient { get; set; }
    }
}
