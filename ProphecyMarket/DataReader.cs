using Basic_Prophecy_Market.Entities.Prophecies;
using Basic_Prophecy_Market.Entities.Recipes;
using Basic_Prophecy_Market.Entities.Uniques;
using Basic_Prophecy_Market.Market;
using System;
using System.Collections;
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

            var lines = Properties.Resources.ProphecyRecipes.Split("\r\n");
            foreach (string line in lines)
            {
                var tokens = CsvLineTokenize(line);
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
            List<RecipeValueAnalysis> list = new List<RecipeValueAnalysis>();
            var lines = Properties.Resources.VendorRecipes.Split("\r\n");
            foreach (string line in lines)
            {
                var tokens = CsvLineTokenize(line);
                VendorUnique item = new VendorUnique { name = tokens[0] };
                item.ResultConditions = GetConstraints(tokens[1]);

                for (int i = 2; i < tokens.Length - 1; i += 2)
                {
                    if (!String.IsNullOrEmpty(tokens[i]))
                    {
                        //build ingredient unique item
                        BaseUnique ingredient = new BaseUnique();
                        ingredient.name = tokens[i];
                        //add list of constraints to item
                        item.Ingredients.Add(ingredient, GetConstraints(tokens[i + 1]));
                    }
                    
                }
                list.Add(new RecipeValueAnalysis(item));
            }

            return list;
        }

        private static List<RecipeConstraint> GetConstraints(string str)
        {
            var test = str.Split(',', StringSplitOptions.RemoveEmptyEntries);
            List<RecipeConstraint> list = new List<RecipeConstraint>();

            foreach (String temp in test)
            {
                string s = temp.Trim();
                if (s == "Normal") { list.Add(RecipeConstraint.Normal); }
                else if (s == "Magic") { list.Add(RecipeConstraint.Magic); }
                else if (s == "Rare") { list.Add(RecipeConstraint.Rare); }
                else if (s == "Unique") { list.Add(RecipeConstraint.Unique); }
                else if (s == "Corrupted") { list.Add(RecipeConstraint.Corrupted); }
                else if (s == "Not Corrupted") { list.Add(RecipeConstraint.Not_Corrupted); }
                else if (s == "Elder") { list.Add(RecipeConstraint.Elder); }
                else if (s == "Shaped") { list.Add(RecipeConstraint.Shaped); }
                else if (s == "x60 Unique Rings") { list.Add(RecipeConstraint.x60_Unique_Rings); }
            }
            return list;
        }

        public static string[] CsvLineTokenize(string s)
        {
            List<string> list = new List<string>();
            int index = 0;
            if (s[s.Length - 1] != ',') s = s + ','; //make sure there is a comma at the end
            while (s.IndexOf(',', index) != -1)
            {
                //check for string literal
                if (index != s.Length - 1 && s[index] == '"')
                {
                    list.Add(s.Substring(index + 1, s.IndexOf('"', index + 1) - index - 1));
                    //move index to end of literal to avoid commas being picked up from string
                    index = s.IndexOf('"', index + 1);
                }
                //check for end of input
                else if (index < s.Length - 1 && s.IndexOf(',', index) != -1 && index != -1)
                {
                    list.Add(s.Substring(index, s.IndexOf(',', index) - index));
                }

                //shift to next token
                index = s.IndexOf(',', index) + 1;
            }
            return list.ToArray();
        }
    }
}
