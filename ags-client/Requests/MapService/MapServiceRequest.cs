using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RestSharp;
using Newtonsoft.Json;
using ags_client.Resources;
using ags_client.Types.Geometry;

namespace ags_client.Requests.MapService
{
    public class MapServiceRequest : BaseRequest
    {
        private string _serviceName;

        public bool? returnUpdates { get; set; }
        public string option { get; set; }
        public SpatialReference outSR { get; set; }

        public MapServiceRequest(string serviceName)
        {
            _serviceName = serviceName;
        }

        public MapServiceResource Execute(AgsClient client, CatalogResource parent) //parent may be the root catalog or a folder catalog
        {
            string resourcePath = String.Format("{0}/{1}/MapServer", parent.resourcePath, _serviceName);
            return Execute(client, resourcePath);
        }

        public MapServiceResource Execute(AgsClient client, string resourcePath) //this overload takes the absolute path - i.e. <Folder>/<service>/MapServer 
        { 
            var request = new RestRequest(resourcePath) { Method = Method.GET };

            if (returnUpdates.HasValue)
                request.AddParameter("returnUpdates", returnUpdates.Value ? "true" : "false");
            if (!String.IsNullOrWhiteSpace(option))
                request.AddParameter("option", option);
            if (outSR != null)
                request.AddParameter("outSR", outSR.wkid);
            
            var result = client.Execute<MapServiceResource>(request, Method.GET);
            result.resourcePath = resourcePath;

            return result;
        }
    }
}
