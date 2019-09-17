using Basic_Prophecy_Market.Net;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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

        public ValueAnalysis(UpgradeProphecy prophecy)
        {
            this.prophecy = prophecy;
            totalCost = 0;
            profit = 0;
            prophecyCost = 0;
            baseCost = 0;
            resultCost = 0;
        }

        public void Update()
        {
            //call api
#if THREAD

            Task<double> b = Task<double>.Run(() => {
                return ApiHandler.GetValueOf(prophecy.baseItem, Program.currentLeague);
            });
            Task<double> r = Task<double>.Run(() => {
                return ApiHandler.GetValueOf(prophecy.baseItem, Program.currentLeague);
            });
            Task<double> p = Task<double>.Run(() => {
                return ApiHandler.GetValueOf(prophecy.baseItem, Program.currentLeague);
            });
            b.Wait();
            r.Wait();
            p.Wait();
            baseCost = b.Result;
            resultCost = r.Result;
            prophecyCost = p.Result;

            //calc values
            totalCost = baseCost + prophecyCost;
            profit = resultCost - totalCost;

#else
            try
            {
                baseCost = ApiHandler.GetValueOf(prophecy.baseItem, Program.currentLeague);
                Console.Write(".");
                resultCost = ApiHandler.GetValueOf(prophecy.resultItem, Program.currentLeague);
                Console.Write(".");
                prophecyCost = ApiHandler.GetValueOf(prophecy, Program.currentLeague);

                //calc values
                totalCost = baseCost + prophecyCost;
                profit = resultCost - totalCost;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

#endif


            Console.WriteLine(" Done.");
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
