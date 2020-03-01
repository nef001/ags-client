using ags_client.Resources;
using ags_client.Resources.GeocodeService;
using RestSharp;
using System.Threading.Tasks;

namespace ags_client.Requests.GeocodeService
{
    public class GeocodeServiceRequest : BaseRequest
    {
        private readonly string _serviceName;

        const string resource = "GeocodeServer";

        public GeocodeServiceRequest(string serviceName)
        {
            _serviceName = serviceName;
        }

        public GeocodeServiceResource Execute(AgsClient client, CatalogResource parent)
        {
            string resourcePath = $"{parent.resourcePath}/{_serviceName}/{resource}";
            return (GeocodeServiceResource)Execute(client, resourcePath);
        }

        public async Task<GeocodeServiceResource> ExecuteAsync(AgsClient client, CatalogResource parent)
        {
            string resourcePath = $"{parent.resourcePath}/{_serviceName}/{resource}";
            var request = createRequest(resourcePath);

            return await client.ExecuteAsync<GeocodeServiceResource>(request, Method.GET);
        }

        //this overload takes the absolute path - e.g rest/services/Locators/MyLocator/GeocodeServer 
        public override BaseResponse Execute(AgsClient client, string resourcePath)
        {
            var request = createRequest(resourcePath);
            return client.Execute<GeocodeServiceResource>(request, Method.GET);
        }

        private RestRequest createRequest(string resourcePath)
        {
            var request = new RestRequest(resourcePath) { Method = Method.GET };
            request.AddParameter("f", "json");
            return request;
        }
    }
}
