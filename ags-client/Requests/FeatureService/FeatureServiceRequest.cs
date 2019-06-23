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

        public FeatureServiceRequest(string serviceName)
        {
            _serviceName = serviceName;
        }

        public FeatureServiceResource Execute(AgsClient client, CatalogResource parent) //parent may be the root catalog or a folder catalog
        {
            string resourcePath = String.Format("{0}/{1}/FeatureServer", parent.resourcePath, _serviceName);
            return Execute(client, resourcePath);
        }

        public FeatureServiceResource Execute(AgsClient client, string resourcePath) //this overload takes the absolute path - i.e. <Folder>/<service>/MapServer 
        {
            var request = new RestRequest(resourcePath) { Method = Method.GET };

            if (!String.IsNullOrWhiteSpace(option))
                request.AddParameter("option", option);
            if (outSR != null)
                request.AddParameter("outSR", outSR.wkid);

            var result = client.Execute<FeatureServiceResource>(request, Method.GET);
            result.resourcePath = resourcePath;

            return result;
        }
    }
}
