using Basic_Prophecy_Market.Entities.Uniques;
using System;
using System.Collections.Generic;
using System.Text;

namespace Basic_Prophecy_Market.Entities.Recipes
{
    class VendorUnique : ResultUnique
    {
        public Dictionary<IItem, List<RecipeConstraint>> Ingredients { get; } = new Dictionary<IItem, List<RecipeConstraint>>();
        public List<RecipeConstraint> ResultConditions { get; set; }
    }
}
