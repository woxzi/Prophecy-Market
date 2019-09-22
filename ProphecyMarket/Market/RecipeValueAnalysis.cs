using Basic_Prophecy_Market.Entities.Recipes;
using Basic_Prophecy_Market.Net;
using System;
using System.Collections.Generic;
using System.Text;

namespace Basic_Prophecy_Market.Market
{
    class RecipeValueAnalysis : ValueAnalysis
    {
        public VendorUnique result;
        public string name
        {
            get
            {
                if (result.ResultConditions.Contains(RecipeConstraint.Shaped))
                {
                    return $"{result.name} (Shaped)";
                }
                else if (result.ResultConditions.Contains(RecipeConstraint.Elder))
                {
                    return $"{result.name} (Elder)";
                }
                else
                {
                    return result.name;
                }
            }
        }
        public string type { get { return "Unique Vendor Recipe"; } }
        public double totalCost { get; set; }
        public double profit { get; set; }
        public bool isUpdated { get; private set; } = false;
        public Dictionary<IItem, double> costs { get; } = new Dictionary<IItem, double>();

        public RecipeValueAnalysis(VendorUnique recipe)
        {
            this.result = recipe;
            totalCost = 0;
            profit = 0;
        }
        public void Update()
        {
            Console.Write($"Retrieving market values for {name}");

            //get price for each ingredient
            totalCost = 0;
            try
            {
                var ingredients = result.Ingredients;
                foreach (var v in ingredients.Keys)
                {
                    Console.Write(".");
                    double cost = ApiHandler.GetValueOf(v, ingredients[v], Program.currentLeague);
                    costs.Add(v, cost);
                    totalCost += cost;
                }

                //calculate profit
                Console.Write(".");
                double income = ApiHandler.GetValueOf(result, Program.currentLeague);
                profit = income - totalCost;

                isUpdated = true;
                Console.WriteLine(" Done.");
            }
            catch (Exception e)
            {

                Console.WriteLine($" Failed.\n\t-{e.Message}");
                isUpdated = false;
            }
        }
        public override string ToString()
        {
            #region Styling

            string spc = "  ";
            string bullet = "- ";

            #endregion

            #region Header

            string header = $"{spc}-----====< {name} >====-----{spc}\n";

            #endregion

            #region Footer

            string footer = spc;
            for (int i = 0; i < header.Length - (2 * spc.Length) - 1; i++)
            {
                footer += "-";
            }
            footer += spc + '\n';

            #endregion

            #region Body
            string body =
            spc + $"Profit: {profit} chaos\n" +
            spc + $"Total Cost: {totalCost} chaos\n" +
            spc + $"Type: {type}\n";
            foreach (var item in costs.Keys)
            {
                body += $"{spc}{bullet}{item.name}: {costs[item]} chaos\n";
            }
            #endregion

            return header + body + footer;
        }
    }
}
