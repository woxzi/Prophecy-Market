using System;
using System.Collections.Generic;
using Basic_Prophecy_Market.Net;
using Newtonsoft.Json;
using RestSharp;

namespace Basic_Prophecy_Market
{
    class Program
    {
        static void Main(string[] args)
        {
            string currentLeague = "Blight";

            Prophecy test = new Prophecy
            {
                name = "The Wealthy Exile"
            };

            BaseUnique test2 = new BaseUnique
            {
                name = "Kaom's Sign"
            };

            ResultUnique test3 = new ResultUnique
            {
                name = "Kaom's Way"
            };

            double value = ApiHandler.GetValueOf(test, currentLeague);

            Console.WriteLine($"The value of {test.name} is {value}");

            //GetLeagues();
            /*
            var client = new RestClient("https://webhook.site/95e441e9-7d82-46b0-aaf2-a71919877bab/");
            var request = new RestRequest("post", Method.POST);
            request.AddHeader("Content-type", "application/json");
            request.AddParameter("application/json", JsonConvert.SerializeObject(test), ParameterType.RequestBody);
            //request.AddJsonBody(test);
            var response = client.Execute(request);

            Console.WriteLine("===============DEBUG================");
            Console.WriteLine(response.ResponseUri);
            Console.WriteLine("Params: ");
            foreach (var p in request.Parameters)
            {
                Console.WriteLine($"\t{p.Name}: {p.Value}");
            }
            Console.WriteLine("Resource: " + request.Resource);
            Console.WriteLine("====================================");
            Console.WriteLine("Status Code: " + response.StatusCode);
            Console.WriteLine("Headers: ");
            foreach (var h in response.Headers)
            {
                Console.WriteLine($"\t{h.Name}: {h.Value}");
            }
            Console.WriteLine(response.Content);
            Console.WriteLine("====================================");
            */
        }

        static List<ProphecyGroup> InitProphecyGroups()
        {
            return null;
        }

        static void GetLeagues()
        {
            var client = RestFactory.GetClient();
            var request = new RestRequest("search/Blight", Method.POST);

            Prophecy test = new Prophecy
            {
                name = "The King's Path"
            };

            string query = new SearchRequest(test).ToString();
            Console.WriteLine("Query: " + query);

            request.AddParameter("application/json", query, ParameterType.RequestBody);
            var response = client.Execute(request);

            Console.WriteLine("Response: "+response.Content);

        }
    }
}
