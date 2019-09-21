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
        public string name { get { return result.name; } }
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
                var ingredients = result.ingredients;
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

            string spacer = "  ";

            #region Header

            string header = spacer + $"-----====< {name} >====-----\n" + spacer;

            #endregion

            #region Footer

            string footer = spacer;
            for (int i = 0; i < header.Length; i++)
            {
                footer += "-";
            }
            footer += spacer + "\n";

            #endregion

            #region Body
            string body =
                $"Profit: {profit} chaos\n" +
                $"Total Cost: {totalCost} chaos\n" +
                $"Type: {type}\n";
            foreach (var item in costs.Keys)
            {
                body += $"\t{item.name}: {costs[item]} chaos\n";
            }
            #endregion

            return header + body + footer;
        }
    }
}
