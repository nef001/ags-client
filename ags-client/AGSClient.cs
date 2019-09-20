using System;
using System.Linq;
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
        readonly IRestClient restSharpClient;

        private Credentials credentials { get; set; }
        private GenerateTokenResource token;
        bool useToken = false;
        string tokenServiceUrl;
        readonly string client_id_type = "requestip";
        readonly int token_request_expiration_minutes = 60;

        public string BaseUrl { get; private set; }

        public ServerInfoResource ServerInfo { get; private set; }

        //anonymous connection to server using http ("agatstgis1.int.atco.com.au", "arcgis", 6080, false, null, null)
        //user connection to server using http ("agatstgis1.int.atco.com.au", "arcgis", 6080, false, username, pwd)
        //user connection to server using https ("agatstgis1.int.atco.com.au", "arcgis", 6443, true, username, pwd)

        //anonymous connection via webadaptor http  ("agatstgis1.int.atco.com.au", "arcgis", null, false, null, null)
        //user connection via webadaptor using http ("agatstgis1.int.atco.com.au", "arcgis", 6080, false, username, pwd)
        //user connection via webadaptor using https ("agatstgis1.int.atco.com.au", "arcgis", null, true, username, pwd) <-port not specified
        public AgsClient(string host, string instance, int? port, bool useSSL, string username, string password)
        {
            bool anonymous = String.IsNullOrWhiteSpace(username);

            var builder = new UriBuilder()
            {
                Scheme = useSSL ? "https" : "http",
                Host = host,
                Port = port ?? port.Value,
                Path = instance,
            };

            Uri baseUrl = builder.Uri;

            restSharpClient = new RestClient(baseUrl).UseSerializer(() => new JsonNetSerializer());
            restSharpClient.AddDefaultHeader("Content-Type", "application/json");

            if (!anonymous) // try to get a token
            {
                credentials = new Credentials { username = username, password = password };

                //1. check if token based security available
                ServerInfoResource serverInfo = new ServerInfoRequest().Execute(this);
                if ((serverInfo.authInfo != null) && (serverInfo.authInfo.isTokenBasedSecurity))
                {
                    tokenServiceUrl = serverInfo.authInfo.tokenServicesUrl;
                    refreshToken(credentials, client_id_type, null, null, token_request_expiration_minutes);
                    useToken = true;

                }
            }


        }

        public AgsClient(string baseUrl)
        {
            BaseUrl = baseUrl; // example base url is "http://agatstgis1.int.atco.com.au/arcgis/rest"
            restSharpClient = new RestClient(BaseUrl).UseSerializer(() => new JsonNetSerializer());
            restSharpClient.AddDefaultHeader("Content-Type", "application/json");

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

            if (useToken && !request.Parameters.Where(p => p.Name.Equals("token")).Any())
            {
                checkAndRefreshToken(credentials, client_id_type, null, null, token_request_expiration_minutes);
                request.AddParameter("token", token.token, ParameterType.QueryString);
            }

            var restResponse = restSharpClient.Execute<T>(request, httpMethod);

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

            if (useToken && !request.Parameters.Where(p => p.Name.Equals("token")).Any())
            {
                checkAndRefreshToken(credentials, client_id_type, null, null, token_request_expiration_minutes);
                request.AddParameter("token", token.token, ParameterType.QueryString);
            }

            var restResponse = await restSharpClient.ExecuteTaskAsync<T>(request, httpMethod);

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

        
        private void refreshToken(Credentials credentials, string client_type, string referer, string ip, int expiration)
        {
            var request = new RestRequest("generateToken") { Method = Method.GET };
            request.AddParameter("f", "json");
            if (!String.IsNullOrWhiteSpace(credentials.username))
                request.AddParameter("username", credentials.username);
            if (!String.IsNullOrWhiteSpace(credentials.password))
                request.AddParameter("password", credentials.password);
            if (!String.IsNullOrWhiteSpace(client_type))
            {
                request.AddParameter("client", client_type);
                switch (client_type)
                {
                    case "referer":
                        request.AddParameter("referer", referer);
                        break;
                    case "ip":
                        request.AddParameter("ip", ip);
                        break;
                    default:
                        break;
                }
            }
            request.AddParameter("expiration", expiration);

            IRestClient client = new RestClient(tokenServiceUrl).UseSerializer(() => new JsonNetSerializer());
            client.AddDefaultHeader("Content-Type", "application/json");
            
            var restResponse = client.Execute<GenerateTokenResource>(request, Method.GET);

            if (restResponse != null)
            {
                var br = restResponse.Data as BaseResponse;
                if (br != null)
                    br.resourcePath = request.Resource;

                token = (GenerateTokenResource)br;
            }

            if (restResponse.ErrorException != null)
            {
                const string message = "Error retrieving response.  Check inner details for more info.";
                var ex = new ApplicationException(message, restResponse.ErrorException);
                throw ex;
            }
        }

        private void checkAndRefreshToken(Credentials credentials, string client, string referer, string ip, int expiration)
        {
            if (token == null)
            {
                return;
            }

            if (token.expires.HasValue)
            {
                TimeSpan ts = DateTime.Now - token.expires.Value;
                if (ts.TotalSeconds <= 0)
                    refreshToken(credentials, client, referer, ip, expiration);
            }
           
        }

    }
}
