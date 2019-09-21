using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Basic_Prophecy_Market.Market;
using Basic_Prophecy_Market.Net;
using Newtonsoft.Json;
using RestSharp;

namespace Basic_Prophecy_Market
{
    class Program
    {
        public static string currentLeague = Properties.Resources.League;
        static void Main(string[] args)
        {
            //create list of all registered profit strategies
            List<ValueAnalysis> list = new List<ValueAnalysis>();
            foreach (var v in DataReader.GetUpgrades())
            {
                list.Add(v);
            }
            foreach (var v in DataReader.GetRecipes())
            {
                list.Add(v);
            }

            UpdateList(list);

            list.Sort((x, y) => x.profit.CompareTo(y.profit));

            foreach (ValueAnalysis v in list)
            {
                if (v.isUpdated)
                {
                    Console.WriteLine(v);
                }
            }
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey(true);
        }

        static void UpdateList(List<ValueAnalysis> list)
        {
            
            foreach (ValueAnalysis v in list)
            {
                v.Update();
            }
        }

    }
}
