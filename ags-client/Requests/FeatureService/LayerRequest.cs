using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RestSharp;
using ags_client.Resources.FeatureService;
using ags_client.Types;

namespace ags_client.Requests.FeatureService
{
    public class LayerRequest<TA> : BaseRequest
        where TA : IRestAttributes
    {
        private int _layerId;

        public bool? returnUpdates { get; set; }

        public LayerRequest(int layerId)
        {
            _layerId = layerId;
        }

        public LayerResource<TA> Execute(AgsClient client, FeatureServiceResource parent) //parent may be the root catalog or a folder catalog
        {
            string resourcePath = String.Format("{0}/{1}", parent.resourcePath, _layerId);
            return Execute(client, resourcePath);
        }

        public LayerResource<TA> Execute(AgsClient client, string resourcePath)
        {
            var request = new RestRequest(resourcePath) { Method = Method.POST };

            if (returnUpdates.HasValue)
                request.AddParameter("returnUpdates", returnUpdates.Value ? "true" : "false");

            var result = client.Execute<LayerResource<TA>>(request, Method.POST);
            result.resourcePath = resourcePath;

            return result;
        }
    }
}
