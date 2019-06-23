using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RestSharp;
using Newtonsoft.Json;
using ags_client.Resources;
using ags_client.Types;
using ags_client.Types.Geometry;

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
            return Execute(client, resourcePath);
        }

        public LayerOrTableResource Execute(AgsClient client, string resourcePath)
        {
            var request = new RestRequest(resourcePath) { Method = Method.POST };

            if (returnUpdates.HasValue)
                request.AddParameter("returnUpdates", returnUpdates.Value ? "true" : "false");

            var result = client.Execute<LayerOrTableResource>(request, Method.POST);
            result.resourcePath = resourcePath;

            return result;
        }
    }
}
