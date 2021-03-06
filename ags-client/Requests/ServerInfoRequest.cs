﻿using ags_client.Resources;
using RestSharp;
using System.Threading.Tasks;

namespace ags_client.Requests
{
    public class ServerInfoRequest : BaseRequest
    {
        public ServerInfoRequest()
        {

        }

        public ServerInfoResource Execute(AgsClient client)
        {
            string resourcePath = "rest/info";
            return (ServerInfoResource)Execute(client, resourcePath);
        }

        public override BaseResponse Execute(AgsClient client, string resourcePath)
        {
            var request = createRequest(resourcePath);
            return client.Execute<ServerInfoResource>(request, Method.GET);
        }

        public async Task<ServerInfoResource> ExecuteAsync(AgsClient client)
        {
            string resourcePath = "rest/info";
            RestRequest request = createRequest(resourcePath);

            return await client.ExecuteAsync<ServerInfoResource>(request, Method.GET);
        }


        private RestRequest createRequest(string resourcePath)
        {
            var request = new RestRequest(resourcePath) { Method = Method.GET };
            request.AddParameter("f", "json");

            return request;
        }
    }
}
