using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RestSharp;
using ags_client.Resources;
using ags_client.Resources.FeatureService;
using ags_client.Types.Geometry;

namespace ags_client.Requests.FeatureService
{
    public class FeatureServiceRequest : BaseRequest
    {
        private string _serviceName;

        public string option { get; set; }
        public SpatialReference outSR { get; set; }

        const string resource = "FeatureServer";

        public FeatureServiceRequest(string serviceName)
        {
            _serviceName = serviceName;
        }

        public FeatureServiceResource Execute(AgsClient client, CatalogResource parent) //parent may be the root catalog or a folder catalog
        {
            string resourcePath = String.Format("{0}/{1}/{2}", parent.resourcePath, _serviceName, resource);
            return (FeatureServiceResource)Execute(client, resourcePath);
        }

        public async Task<FeatureServiceResource> ExecuteAsync(AgsClient client, CatalogResource parent)
        {
            string resourcePath = String.Format("{0}/{1}/{2}", parent.resourcePath, _serviceName, resource);
            var request = createRequest(resourcePath);

            return await client.ExecuteAsync<FeatureServiceResource>(request, Method.POST);
        }

        public override BaseResponse Execute(AgsClient client, string resourcePath) //this overload takes the absolute path - i.e. rest/services/<Folder>/<service>/MapServer 
        {
            var request = createRequest(resourcePath);
            return client.Execute<FeatureServiceResource>(request, Method.GET);
        }

        private RestRequest createRequest(string resourcePath)
        {
            var request = new RestRequest(resourcePath) { Method = Method.POST };

            if (!String.IsNullOrWhiteSpace(option))
                request.AddParameter("option", option);
            if (outSR != null)
                request.AddParameter("outSR", outSR.wkid);

            request.AddParameter("f", "json");

            return request;
        }
    }
}
