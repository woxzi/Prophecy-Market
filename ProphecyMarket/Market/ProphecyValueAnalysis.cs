using Basic_Prophecy_Market.Entities.Prophecies;
using Basic_Prophecy_Market.Net;
using System;
using System.Collections.Generic;
using System.Text;

namespace Basic_Prophecy_Market.Market
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
