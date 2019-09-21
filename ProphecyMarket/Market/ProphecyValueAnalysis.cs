using Basic_Prophecy_Market.Net;
using System;
using System.Collections.Generic;
using System.Text;

namespace Basic_Prophecy_Market
{
    class ProphecyValueAnalysis : ValueAnalysis
    {
        public UpgradeProphecy prophecy { get; set; }
        public double totalCost { get; set; }
        public double profit { get; set; }
        public string name { get { return prophecy.name; } }
        public string type { get { return "Upgrade Prophecy"; } }
        public Dictionary<IItem, double> costs { get; } = new Dictionary<IItem, double>();

        public bool isUpdated { get; private set; } = false;

        public ProphecyValueAnalysis(UpgradeProphecy prophecy)
        {
            this.prophecy = prophecy;
            totalCost = 0;
            profit = 0;
        }

        public void Update()
        {
            Console.Write($"Retrieving market values for {name}");

            //call api
            try
            {
                Console.Write(".");
                costs.Add(prophecy.baseItem, ApiHandler.GetValueOf(prophecy.baseItem, Program.currentLeague));
                Console.Write(".");
                costs.Add(prophecy.resultItem, ApiHandler.GetValueOf(prophecy.resultItem, Program.currentLeague));
                Console.Write(".");
                costs.Add(prophecy, ApiHandler.GetValueOf(prophecy, Program.currentLeague));

                //calc values
                totalCost = costs[prophecy.baseItem] + costs[prophecy];
                profit = costs[prophecy.resultItem] - totalCost;

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

            string spacer = "  ";

            //do header
            string header = spacer + $"-----====< {name} >====-----\n" + spacer;

            //do footer
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
