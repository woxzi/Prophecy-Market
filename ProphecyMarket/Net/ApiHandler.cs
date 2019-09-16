using Newtonsoft.Json;
using RestSharp;
using System;

namespace Basic_Prophecy_Market.Net
{
    class ApiHandler
    {
        //returns item value in chaos orbs
        public static double GetValueOf(Item item, string league)
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
                        size++;
                    }
                }
                return total / 5;
            }

            return -1;
        }
    }
}
