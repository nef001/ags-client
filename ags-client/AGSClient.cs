using System;


using RestSharp;


using ags_client.Operations;
using ags_client.Utility;
using ags_client.Types;
using ags_client.Types.Geometry;

namespace ags_client

{
    public class AgsClient
    {
        readonly IRestClient _client;

        public AgsClient(string baseUrl)
        {
            BaseUrl = baseUrl;
            _client = new RestClient(BaseUrl).UseSerializer(() => new JsonNetSerializer()); 
        }

        public string BaseUrl { get; private set; }

        public T Execute<T>(RestRequest request, Method httpMethod) where T : new()
        {
            request.AddParameter("f", "json"); // used on every request

            var response = _client.Execute<T>(request, httpMethod);
            
            if (response.ErrorException != null)
            {
                const string message = "Error retrieving response.  Check inner details for more info.";
                var ex = new ApplicationException(message, response.ErrorException);
                throw ex;
            }

            return response.Data;
        }




    }
}
