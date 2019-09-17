using Basic_Prophecy_Market.Entities;
using Newtonsoft.Json;
using RestSharp;
using System;

namespace Basic_Prophecy_Market.Net
{
    class ApiHandler
    {
        //returns item value in chaos orbs
        public static double GetValueOf(IItem item, string league)
        {
            bool debug = false;

            var client = RestFactory.GetClient();
            var request = RestFactory.GetSearchRequest(item, league);
            var response = client.Execute<SearchResult>(request);

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
                Console.WriteLine($"Could not retrieve listings for {item.name}");
                Console.WriteLine(response.Content);
            }
            else
            {
                //convert response to searchresult object for use in logic
                SearchResult result = JsonConvert.DeserializeObject<SearchResult>(response.Content);

                double total = 0;
                int size = 5;

                //check the lowest five result values
                for (int i = 0; i < size; i++)
                {
                    var req = RestFactory.GetListingRequest(result.result[i], result.id);
                    var resp = client.Execute(req);

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
                return total / 5;
            }

            return -1;
        }
        //find the value in chaos orbs of the given currency
        public static double FindChaosValue(string currencyType, double amount, string league)
        {
            var client = RestFactory.GetClient();
            var request = RestFactory.GetExchangeRequest(new Currency { Type = currencyType }, new Currency { Type = "chaos" }, league);
            var response = client.Execute<SearchResult>(request);

            //convert response to searchresult object for use in logic
            SearchResult result = JsonConvert.DeserializeObject<SearchResult>(response.Content);

            //average most recent 3 exchange listings
            double totalCost = 0;

            for (int i = 0; i < 3; i++)
            {
                var req = RestFactory.GetListingRequest(result.result[i], result.id);
                var resp = client.Execute(req);

                dynamic listing = JsonConvert.DeserializeObject(resp.Content);
                double test = listing.result[0].listing.price.amount;
                totalCost = test;
            }


            return amount * totalCost / 3;
        }
    }
}
