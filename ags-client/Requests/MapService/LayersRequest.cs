using ags_client.Resources.MapService;
using ags_client.Types;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ags_client.Requests.MapService
{
    public class LayersRequest : BaseRequest
    {
        public List<Layer> dynamicLayers { get; set; }

        const string resource = "layers";

        public LayersResource Execute(AgsClient client, MapServiceResource parent) //parent may be the root catalog or a folder catalog
        {
            string resourcePath = $"{parent.resourcePath}/{resource}";
            return (LayersResource)Execute(client, resourcePath);
        }

        public async Task<LayersResource> ExecuteAsync(AgsClient client, MapServiceResource parent)
        {
            string resourcePath = $"{parent.resourcePath}/{resource}";
            var request = createRequest(resourcePath);

            return await client.ExecuteAsync<LayersResource>(request, Method.POST);
        }

        public override BaseResponse Execute(AgsClient client, string resourcePath)
        {
            var request = createRequest(resourcePath);
            return client.Execute<LayersResource>(request, Method.POST);

        }

        private RestRequest createRequest(string resourcePath)
        {
            var request = new RestRequest(resourcePath) { Method = Method.POST };

            var jss = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };

            if ((dynamicLayers != null) && (dynamicLayers.Count > 0))
                request.AddParameter("dynamicLayers", JsonConvert.SerializeObject(dynamicLayers, jss));

            request.AddParameter("f", "json");

            return request;
        }
    }
}
