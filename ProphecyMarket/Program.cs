using System;
using System.Collections.Generic;
using Basic_Prophecy_Market.Net;
using Newtonsoft.Json;
using RestSharp;

namespace Basic_Prophecy_Market
{
    class Program
    {
        public static string currentLeague = "Blight";
        static void Main(string[] args)
        {
            List<ValueAnalysis> list = InitUpgrades();

            foreach(ValueAnalysis v in list)
            {
                v.Update();
                Console.WriteLine(v);
            }
        }

        //intitializes the list of prophecies, without market data
        static List<ValueAnalysis> InitUpgrades()
        {
            List<ValueAnalysis> list = new List<ValueAnalysis>();

            #region King's Path
            BaseUnique baseItem = new BaseUnique
            {
                name = "Kaom's Sign",
                type = "Coral Ring"
            };

            ResultUnique resultItem = new ResultUnique
            {
                name = "Kaom's Way",
                type = "Coral Ring"
            };

            UpgradeProphecy prophecy = new UpgradeProphecy
            {
                name = "The King's Path",
                baseItem = baseItem,
                resultItem = resultItem
            };

            list.Add(new ValueAnalysis
            {
                prophecy = prophecy,
                totalCost = 0,
                profit = 0
            });
            #endregion

            return list;
        }

    }
}
