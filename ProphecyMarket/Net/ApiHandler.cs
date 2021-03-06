﻿using Basic_Prophecy_Market.Entities;
using Basic_Prophecy_Market.Entities.Recipes;
using System.Collections.Generic;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Threading.Tasks;

namespace Basic_Prophecy_Market.Net
{
    class ApiHandler
    {
        private static int callWaitMilliseconds = 500;

        //stores the value of each currency with respect to chaos
        public static Dictionary<string, double> currencyValue = new Dictionary<string, double>();

        //returns item value in chaos orbs
        public static double GetValueOf(IItem item, string league)
        {
            bool debug = false;
            int samplesize = System.Convert.ToInt32(Properties.Resources.SampleSize);

            var client = RestFactory.GetClient();
            var request = RestFactory.GetSearchRequest(item, league);
            var response = client.Execute<SearchResult>(request);
            System.Threading.Thread.Sleep(callWaitMilliseconds);

            //print response if debug is enabled
            if (debug)
            {
                Console.WriteLine("===============DEBUG================");
                Console.WriteLine("Status Code: " + response.StatusCode);
                Console.WriteLine(response.Content);
                Console.WriteLine("====================================");
            }

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                string s = $"Could not retrieve listings for {item.name}\n\tStatus code: {response.StatusCode}\n{response.Content}";
                throw new Exception(s);
            }
            else
            {
                //convert response to searchresult object for use in logic
                SearchResult result = JsonConvert.DeserializeObject<SearchResult>(response.Content);

                double total = 0;

                //check the lowest result values
                for (int i = 0; i < samplesize; i++)
                {
                    var req = RestFactory.GetListingRequest(result.result[i], result.id);
                    var resp = client.Execute(req);
                    System.Threading.Thread.Sleep(callWaitMilliseconds);
                    dynamic listing = JsonConvert.DeserializeObject(resp.Content);

                    if (listing.result[0].listing.price.currency == "chaos")
                    {
                        double test = listing.result[0].listing.price.amount;
                        total += test;
                    }
                    else
                    {
                        string currency = listing.result[0].listing.price.currency;
                        double amount = listing.result[0].listing.price.amount;
                        total += FindChaosValue(currency, amount, league);
                    }
                }
                return total / samplesize;
            }
        }

        //returns item value in chaos, given specific constraints, from vendor recipes
        public static double GetValueOf(IItem item, List<RecipeConstraint> constraints, string league)
        {
            throw new NotImplementedException();
        }
        //find the value in chaos orbs of the given currency
        public static double FindChaosValue(string currencyType, double amount, string league)
        {
            //chaos is always equal to chaos
            if (currencyType == "chaos") return 1;
            //reuse old values if created during current execution
            if (currencyValue.ContainsKey(currencyType))
            {
                return amount * currencyValue[currencyType];
            }

            int samplesize = System.Convert.ToInt32(Properties.Resources.SampleSize);

            var client = RestFactory.GetClient();
            var request = RestFactory.GetExchangeRequest(new Currency { name = "chaos" }, new Currency { name = currencyType }, league);
            var response = client.Execute<SearchResult>(request);
            System.Threading.Thread.Sleep(callWaitMilliseconds);

            //convert response to searchresult object for use in logic
            SearchResult result = JsonConvert.DeserializeObject<SearchResult>(response.Content);

            //average most recent exchange listings
            double totalCost = 0;
            int offset = 0;
            for (int i = 0; i < samplesize; i++)
            {
                //if result is null, skip it/
                if (result != null)
                {
                    var req = RestFactory.GetListingRequest(result.result[i + offset], result.id);
                    var resp = client.Execute(req);
                    System.Threading.Thread.Sleep(callWaitMilliseconds);

                    dynamic listing = JsonConvert.DeserializeObject(resp.Content);
                    double test = listing.result[0].listing.price.amount;
                    totalCost += test;
                }
                else
                {
                    offset++;
                    i--;
                }
            }
            
            currencyValue[currencyType] = totalCost / samplesize;

            return amount * currencyValue[currencyType];
        }


    }
}
