using System;
using System.Collections.Generic;

using RestSharp;
using Newtonsoft.Json;
using ags_client.Resources.MapService;
using ags_client.Types;

namespace ags_client.Requests.MapService
{
    public class LayersRequest : BaseRequest
    {
        public List<Layer> dynamicLayers { get; set; }

        public LayersResource Execute(AgsClient client, MapServiceResource parent) //parent may be the root catalog or a folder catalog
        {
            string resourcePath = String.Format("{0}/layers", parent.resourcePath);
            return Execute(client, resourcePath);
        }

        public LayersResource Execute(AgsClient client, string resourcePath)
        {
            var request = new RestRequest(resourcePath) { Method = Method.POST };

            var jss = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };

            if ((dynamicLayers != null) && (dynamicLayers.Count > 0))
                request.AddParameter("dynamicLayers", JsonConvert.SerializeObject(dynamicLayers, jss));

            var result = client.Execute<LayersResource>(request, Method.POST);
            result.resourcePath = resourcePath;

            return result;
        }
    }
}
