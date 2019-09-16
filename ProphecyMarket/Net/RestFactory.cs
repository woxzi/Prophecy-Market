using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using RestSharp;
namespace Basic_Prophecy_Market.Net
{
    class RestFactory
    {
        public static RestClient GetClient()
        {
            var client = new RestClient("https://www.pathofexile.com/api/trade/");
            //var client = new RestClient("https://57911348.ngrok.io/api/trade/");
            //var client = new RestClient("https://webhook.site/95e441e9-7d82-46b0-aaf2-a71919877bab/");
            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            return client;
        }

        //Returns a post request for a given item
        public static RestRequest GetSearchRequest(Item item, string league)
        {
            var request = new RestRequest($"search/{league}", Method.POST);
            request.AddParameter("application/json; charset=utf-8", JsonConvert.SerializeObject(new SearchRequest(item)), ParameterType.RequestBody);
            //request.AddJsonBody(new SearchRequest(item));
            return request;
        }

        //Returns a get request for a given listing
        public static RestRequest GetListingRequest(string listingId, string queryId)
        {
            return new RestRequest($"fetch/{listingId}?query={queryId}", Method.GET);
        }
    }

    class SearchRequest
    {

        //create a variable type object for query, prep for serialization
        public SearchRequest(Item item) => query = new
        {
            status = new
            {
                option = "online"
            },
            item.name,
            item.type
        };

        public object query;

        public object sort = new
        {
            price = "asc"
        };

        //returns a json string to use as a poe trade api request
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    class SearchResult
    {
        public string[] result;
        public string id;
        public int total;
    }
}
