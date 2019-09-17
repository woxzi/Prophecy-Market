using Basic_Prophecy_Market.Net;
using System;
using System.Collections.Generic;
using System.Text;

namespace Basic_Prophecy_Market
{
    class ValueAnalysis
    {
        public UpgradeProphecy prophecy { get; set; }
        public double totalCost { get; set; }
        public double profit { get; set; }
        public double prophecyCost { get; set; }
        public double baseCost { get; set; }
        public double resultCost { get; set; }

        public void Update()
        {
            prophecyCost = ApiHandler.GetValueOf(prophecy, Program.currentLeague);
            baseCost = ApiHandler.GetValueOf(prophecy.baseItem, Program.currentLeague);
            resultCost = ApiHandler.GetValueOf(prophecy.resultItem, Program.currentLeague);

            totalCost = baseCost + prophecyCost;
            profit = resultCost - totalCost;
        }

        public override string ToString()
        {
            return $"Total Cost: {totalCost} chaos\n" +
                $"Profit: {profit} chaos\n" +
                $"\t{prophecy.name}: {prophecyCost} chaos\n" +
                $"\t{prophecy.baseItem.name}: {baseCost} chaos\n" +
                $"\t{prophecy.resultItem.name}: {resultCost} chaos\n";
        }
    }
}
