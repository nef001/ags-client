using System;
using System.Threading.Tasks;

using RestSharp;
using ags_client.Resources;
using ags_client.Resources.GeometryService;

namespace ags_client.Requests.GeometryService
{
    public class GeometryServiceRequest : BaseRequest
    {
        private readonly string _serviceName;

        const string resource = "GeometryServer";

        public GeometryServiceRequest(string serviceName) //typically Geometry
        {
            _serviceName = serviceName;
        }

        public GeometryServiceResource Execute(AgsClient client, CatalogResource parent) //parent is typically the Utilities folder
        {
            string resourcePath = $"{parent.resourcePath}/{_serviceName}/{resource}";
            return (GeometryServiceResource)Execute(client, resourcePath);
        }

        public async Task<GeometryServiceResource> ExecuteAsync(AgsClient client, CatalogResource parent)
        {
            string resourcePath = $"{parent.resourcePath}/{_serviceName}/{resource}";
            var request = createRequest(resourcePath);

            return await client.ExecuteAsync<GeometryServiceResource>(request, Method.GET);
        }

        public override BaseResponse Execute(AgsClient client, string resourcePath) //this overload takes the absolute path - typically rest/services/Utilities/Geometry/GeometryServer 
        {
            var request = createRequest(resourcePath);
            return client.Execute<GeometryServiceResource>(request, Method.GET);
        }

        private RestRequest createRequest(string resourcePath)
        {
            var request = new RestRequest(resourcePath) { Method = Method.GET };
            request.AddParameter("f", "json");
            return request;
        }
    }
}
