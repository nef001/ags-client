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
                Port = port ?? 80,
                Path = instance,
            };

            Uri baseUrl = builder.Uri;

            restSharpClient = new RestClient(baseUrl).UseSerializer(() => new JsonNetSerializer());
            restSharpClient.AddDefaultHeader("Content-Type", "application/x-www-form-urlencoded");

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

        public T Execute<T>(RestRequest request, Method httpMethod) where T : new()
        {
            //request.JsonSerializer = new JsonNetSerializer();
            request.AddHeader("Accept", "text/html, application/xhtml+xml, */*");

            if (useToken && !request.Parameters.Where(p => p.Name.Equals("token")).Any())
            {
                checkAndRefreshToken(credentials, client_id_type, null, null, token_request_expiration_minutes);
                request.AddParameter("token", token.token, ParameterType.GetOrPost);
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
            if (useToken && !request.Parameters.Where(p => p.Name.Equals("token")).Any())
            {
                checkAndRefreshToken(credentials, client_id_type, null, null, token_request_expiration_minutes);
                request.AddParameter("token", token.token, ParameterType.QueryString);
            }

            var restResponse = await restSharpClient.ExecuteAsync<T>(request, httpMethod);

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
            var request = new RestRequest("generateToken"); // { Method = Method.POST };
            request.JsonSerializer = new JsonNetSerializer();

            if (!String.IsNullOrWhiteSpace(credentials.username))
                request.AddParameter("username", credentials.username, ParameterType.GetOrPost);
            if (!String.IsNullOrWhiteSpace(credentials.password))
                request.AddParameter("password", credentials.password, ParameterType.GetOrPost);
            if (!String.IsNullOrWhiteSpace(client_type))
            {
                request.AddParameter("client", client_type, ParameterType.GetOrPost);
                switch (client_type)
                {
                    case "referer":
                        request.AddParameter("referer", referer, ParameterType.GetOrPost);
                        break;
                    case "ip":
                        request.AddParameter("ip", ip, ParameterType.GetOrPost);
                        break;
                    default:
                        break;
                }
            }
            request.AddParameter("expiration", expiration, ParameterType.GetOrPost);

            IRestClient client = new RestClient(tokenServiceUrl).UseSerializer(() => new JsonNetSerializer());
            client.AddDefaultHeader("Content-Type", "application/json");

            var restResponse = client.Execute<GenerateTokenResource>(request);

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
                TimeSpan ts = token.expires.Value - DateTime.Now.ToUniversalTime();
                if (ts.TotalSeconds <= 0)
                    refreshToken(credentials, client, referer, ip, expiration);
            }
           
        }

    }
}
