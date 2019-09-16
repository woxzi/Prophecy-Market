using System;
using System.Collections.Generic;
using RestSharp;

namespace Basic_Prophecy_Market
{
    class Program
    {
        static void Main(string[] args)
        {
            string currentLeague = "Blight";
            var client = new RestClient("https://www.pathofexile.com/api/trade/");
            // client.Authenticator = new HttpBasicAuthenticator(username, password);



            Console.WriteLine("Hello World!");
        }

        static List<ProphecyGroup> InitProphecies()
        {
            return null;
        }
    }
}
