using System;
using System.Threading;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;
using ags_client.Utility;
using ags_client.Types;
using ags_client.Resources;
using ags_client.Requests;

namespace ags_client

{
    public class AgsClient
    {
        readonly IRestClient _client;

        //string token = "yuLxFjVmAOCs9Sm--nHSierZSTRVzmBsjLk4kQC2peMBkyWvK64qgIElT7fIHs0pcHcs__WD6mqJZxNJjYV3NgEAWJJnhwqMYqCLd0nhinrMXAcmqwhocyZFHJcrioo72x2gQUWaaXDKxZPP6ShEYg..";


        private AccessToken _accessToken;

        public string BaseUrl { get; private set; }

        public ServerInfoResource ServerInfo { get; private set; }


        public AgsClient(string baseUrl)
        {
            BaseUrl = baseUrl; // example base url is "http://agatstgis1.int.atco.com.au/arcgis/rest"
            _client = new RestClient(BaseUrl).UseSerializer(() => new JsonNetSerializer());
            _client.AddDefaultHeader("Content-Type", "application/json");

            ServerInfo = new ServerInfoRequest().Execute(this);
        }



        //public void RefreshToken(string tokenUrl, string clientID, string clientSecret)
        //{
        //    // https://www.arcgis.com/sharing/rest/oauth2/token

        //    var client = new RestClient(tokenUrl).UseSerializer(() => new JsonNetSerializer());
        //    client.AddDefaultHeader("Content-Type", "application/x-www-form-urlencoded");
        //    //client.Authenticator = new HttpBasicAuthenticator(clientID, clientSecret);
        //    var request = new RestRequest() { Method = Method.POST };
        //    request.AddHeader("Accept", "application/json");
        //    request.AddParameter("f", "json");
        //    request.AddParameter("client_id", clientID);
        //    request.AddParameter("client_secret", clientSecret);
        //    request.AddParameter("grant_type", "client_credentials");
        //    request.AddParameter("expiration", 20160);
        //    var response = client.Execute<AccessToken>(request);
        //    if (response.ErrorException != null)
        //    {
        //        const string message = "Error retrieving response.  Check inner details for more info.";
        //        var ex = new ApplicationException(message, response.ErrorException);
        //        throw ex;
        //    }
        //    _accessToken = response.Data;
        //}

        

        public T Execute<T>(RestRequest request, Method httpMethod) where T : new()
        {
            request.AddHeader("Accept", "application/json");

            request.AddParameter("f", "json"); // used on every request

            if (_accessToken != null)
                request.AddParameter("token", _accessToken.access_token, ParameterType.QueryString);


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
