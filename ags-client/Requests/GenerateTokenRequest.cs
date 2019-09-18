using System;
using System.Threading.Tasks;
using RestSharp;
using ags_client.Resources;


namespace ags_client.Requests
{
    public class GenerateTokenRequest : BaseRequest
    {
        public string username { get; set; }
        public string password { get; set; }
        public string client { get; set; } // value needs to be "referer", "ip", or "requestip"
        public string referer { get; set; }
        public string ip { get; set; }
        public int expiration { get; set; } //in minutes

        public GenerateTokenResource Execute(AgsClient client)
        {
            // https://agatstgis1.int.atco.com.au/arcgis/tokens/generateToken
            string resourcePath = "tokens/generateToken";
            return (GenerateTokenResource)Execute(client, resourcePath);
        }

        public override BaseResponse Execute(AgsClient client, string resourcePath)
        {
            var request = createRequest(resourcePath);
            return client.Execute<GenerateTokenResource>(request, Method.POST);
        }

        public async Task<GenerateTokenResource> ExecuteAsync(AgsClient client)
        {
            string resourcePath = "tokens/generateToken";
            RestRequest request = createRequest(resourcePath);

            return await client.ExecuteAsync<GenerateTokenResource>(request, Method.POST);
        }


        private RestRequest createRequest(string resourcePath)
        {
            var request = new RestRequest(resourcePath) { Method = Method.POST };

            if (!String.IsNullOrWhiteSpace(username))
                request.AddParameter("username", username);
            if (!String.IsNullOrWhiteSpace(password))
                request.AddParameter("password", password);
            if (!String.IsNullOrWhiteSpace(client))
            {
                request.AddParameter("client", client);
                switch (client)
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
            return request;
        }
    }
}
