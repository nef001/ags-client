using System;
using System.Threading.Tasks;
using RestSharp;
using ags_client.Resources.MapService;

namespace ags_client.Requests.MapService
{
    public class LayerOrTableRequest : BaseRequest
    {
        private int _layerOrTableId;

        public bool? returnUpdates { get; set; }

        public LayerOrTableRequest(int layerOrTableId)
        {
            _layerOrTableId = layerOrTableId;
        }

        public LayerOrTableResource Execute(AgsClient client, MapServiceResource parent) //parent may be the root catalog or a folder catalog
        {
            string resourcePath = String.Format("{0}/{1}", parent.resourcePath, _layerOrTableId);
            return (LayerOrTableResource)Execute(client, resourcePath);
        }

        public async Task<LayerOrTableResource> ExecuteAsync(AgsClient client, MapServiceResource parent)
        {
            string resourcePath = String.Format("{0}/{1}", parent.resourcePath, _layerOrTableId);
            var request = createRequest(resourcePath);

            return await client.ExecuteAsync<LayerOrTableResource>(request, Method.POST);
        }

        public override BaseResponse Execute(AgsClient client, string resourcePath)
        {
            var request = createRequest(resourcePath);
            var result = client.Execute<LayerOrTableResource>(request, Method.GET);

            return result;
        }

        private RestRequest createRequest(string resourcePath)
        {
            var request = new RestRequest(resourcePath) { Method = Method.POST };

            if (returnUpdates.HasValue)
                request.AddParameter("returnUpdates", returnUpdates.Value ? "true" : "false");

            return request;
        }
    }
}
