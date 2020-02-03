using System;
using System.Threading.Tasks;
using RestSharp;
using ags_client.Resources.MapService;

namespace ags_client.Requests.MapService
{
    public class LayerOrTableRequest : BaseRequest
    {
        private readonly int _layerOrTableId;

        public bool? returnUpdates { get; set; }

        public LayerOrTableRequest(int layerOrTableId)
        {
            _layerOrTableId = layerOrTableId;
        }

        public LayerOrTableResource Execute(AgsClient client, MapServiceResource parent) //parent may be the root catalog or a folder catalog
        {
            string resourcePath = $"{parent.resourcePath}/{_layerOrTableId}";
            return (LayerOrTableResource)Execute(client, resourcePath);
        }

        public async Task<LayerOrTableResource> ExecuteAsync(AgsClient client, MapServiceResource parent)
        {
            string resourcePath = $"{parent.resourcePath}/{_layerOrTableId}";
            var request = createRequest(resourcePath);

            return await client.ExecuteAsync<LayerOrTableResource>(request, Method.GET);
        }

        public override BaseResponse Execute(AgsClient client, string resourcePath)
        {
            var request = createRequest(resourcePath);
            return client.Execute<LayerOrTableResource>(request, Method.GET);
        }

        private RestRequest createRequest(string resourcePath)
        {
            var request = new RestRequest(resourcePath) { Method = Method.GET };

            if (returnUpdates.HasValue)
                request.AddParameter("returnUpdates", returnUpdates.Value ? "true" : "false");

            request.AddParameter("f", "json");

            return request;
        }
    }
}
