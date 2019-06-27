using System;
using System.Threading;
using System.Threading.Tasks;
using RestSharp;
using ags_client.Utility;

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

            var restResponse = _client.Execute<T>(request, httpMethod);

            if (restResponse != null)
            {
                var br = restResponse.Data as BaseResponse;
                if (br != null)
                    br.resourcePath = request.Resource;
            }
            
            if (restResponse.ErrorException != null)
            {
                const string message = "Error retrieving response.  Check inner details for more info.";
                var ex = new ApplicationException(message, restResponse.ErrorException);
                throw ex;
            }

            return restResponse.Data;
        }

        public async Task<T> ExecuteAsync<T>(RestRequest request, Method httpMethod) where T : new()
        {
            request.AddParameter("f", "json"); // used on every request

            var restResponse = await _client.ExecuteTaskAsync<T>(request, httpMethod);

            if (restResponse != null)
            {
                var br = restResponse.Data as BaseResponse;
                if (br != null)
                    br.resourcePath = request.Resource;
            }

            if (restResponse.ErrorException != null)
            {
                const string message = "Error retrieving response.  Check inner details for more info.";
                var ex = new ApplicationException(message, restResponse.ErrorException);
                throw ex;
            }

            return restResponse.Data;
        }

        


    }
}
