using System;
using System.Threading.Tasks;

using RestSharp;
using ags_client.Resources;
using ags_client.Resources.GeometryService;

namespace ags_client.Requests.GeometryService
{
    public class GeometryServiceRequest : BaseRequest
    {
        private string _serviceName;

        public GeometryServiceRequest(string serviceName) //typically Geometry
        {
            _serviceName = serviceName;
        }

        public GeometryServiceResource Execute(AgsClient client, CatalogResource parent) //parent is typically the Utilities folder
        {
            string resourcePath = String.Format("{0}/{1}/GeometryServer", parent.resourcePath, _serviceName);
            return (GeometryServiceResource)Execute(client, resourcePath);
        }

        public override BaseResponse Execute(AgsClient client, string resourcePath) //this overload takes the absolute path - typically Utilities/Geometry/GeometryServer 
        {
            var request = createRequest(resourcePath);
            var result = client.Execute<GeometryServiceResource>(request, Method.GET);
            result.resourcePath = resourcePath;

            return result;
        }

        private RestRequest createRequest(string resourcePath)
        {
            var request = new RestRequest(resourcePath) { Method = Method.POST };
            return request;
        }
    }
}
