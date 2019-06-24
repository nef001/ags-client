using System;

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

        public override BaseResponse Execute(AgsClient client, string resourcePath)
        {
            var request = new RestRequest(resourcePath) { Method = Method.GET };

            if (returnUpdates.HasValue)
                request.AddParameter("returnUpdates", returnUpdates.Value ? "true" : "false");

            var result = client.Execute<LayerOrTableResource>(request, Method.GET);
            result.resourcePath = resourcePath;

            return result;
        }
    }
}
