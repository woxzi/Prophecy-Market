using Basic_Prophecy_Market.Market;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Basic_Prophecy_Market
{
    class DataReader
    {
        public static List<ProphecyValueAnalysis> GetUpgrades()
        {
            List<ProphecyValueAnalysis> list = new List<ProphecyValueAnalysis>();

            var propcsv = Properties.Resources.ProphecyList;

            var lines = propcsv.Split("\r\n");
            foreach (string line in lines)
            {
                var tokens = line.Split(',');

                BaseUnique baseItem = new BaseUnique
                {
                    name = tokens[1]
                };

                ResultUnique resultItem = new ResultUnique
                {
                    name = tokens[2]
                };

                UpgradeProphecy prophecy = new UpgradeProphecy
                {
                    name = tokens[0],
                    resultItem = resultItem,
                    baseItem = baseItem
                };

                list.Add(new ProphecyValueAnalysis(prophecy));
            }

            return list;
        }
        public static List<RecipeValueAnalysis> GetRecipes()
        {
            throw new NotImplementedException();
        }
    }
}
