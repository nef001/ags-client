using System;
using System.Threading.Tasks;
using RestSharp;
using ags_client.Resources;
using ags_client.Resources.MapService;
using ags_client.Types.Geometry;

namespace ags_client.Requests.MapService
{
    public class MapServiceRequest : BaseRequest
    {
        private string _serviceName;

        public bool? returnUpdates { get; set; }
        public string option { get; set; }
        public SpatialReference outSR { get; set; }

        const string resource = "MapServer";

        public MapServiceRequest(string serviceName)
        {
            _serviceName = serviceName;
        }

        public MapServiceResource Execute(AgsClient client, CatalogResource parent) //parent may be the root catalog or a folder catalog
        {
            string resourcePath = String.Format("{0}/{1}/{2}", parent.resourcePath, _serviceName, resource);
            return (MapServiceResource)Execute(client, resourcePath);
        }

        public async Task<MapServiceResource> ExecuteAsync(AgsClient client, MapServiceResource parent)
        {
            string resourcePath = String.Format("{0}/{1}/{2}", parent.resourcePath, _serviceName, resource);
            var request = createRequest(resourcePath);

            return await client.ExecuteAsync<MapServiceResource>(request, Method.POST);
        }

        public override BaseResponse Execute(AgsClient client, string resourcePath) //this overload takes the absolute path - i.e. <Folder>/<service>/MapServer 
        {
            var request = createRequest(resourcePath);
            return client.Execute<MapServiceResource>(request, Method.GET);
        }

        private RestRequest createRequest(string resourcePath)
        {
            var request = new RestRequest(resourcePath) { Method = Method.POST };

            if (returnUpdates.HasValue)
                request.AddParameter("returnUpdates", returnUpdates.Value ? "true" : "false");
            if (!String.IsNullOrWhiteSpace(option))
                request.AddParameter("option", option);
            if (outSR != null)
                request.AddParameter("outSR", outSR.wkid);

            return request;
        }
    }
}
