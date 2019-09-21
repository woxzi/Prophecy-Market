using System;
using System.Collections.Generic;
using System.Text;

namespace Basic_Prophecy_Market.Entities.Recipes
{
    class VendorUnique : ResultUnique
    {
        public Dictionary<IItem, HashSet<RecipeConstraint>> ingredients { get; } = new Dictionary<IItem, HashSet<RecipeConstraint>>();
    }
}
