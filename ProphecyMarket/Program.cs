using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
            List<ValueAnalysis> list = DataReader.GetUpgrades();

            UpdateList(list);

            list.Sort((x, y) => x.profit.CompareTo(y.profit));

            foreach (ValueAnalysis v in list)
            {
                Console.WriteLine(v);
            }
        }

        static void UpdateList(List<ValueAnalysis> list)
        {
            foreach (ValueAnalysis v in list)
            {
                Console.Write($"Retrieving market values for {v.prophecy.name}.");
                v.Update();
            }
        }

    }
}
