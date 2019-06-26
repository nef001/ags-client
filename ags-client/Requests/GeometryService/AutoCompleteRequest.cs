using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using RestSharp;
using Newtonsoft.Json;
using ags_client.Resources.GeometryService;
using ags_client.Types.Geometry;

namespace ags_client.Requests.GeometryService
{
    public class AutoCompleteRequest : BaseRequest
    {
        public List<Polygon> polygons { get; set; }
        public List<Polyline> polylines { get; set; }
        public SpatialReference sr { get; set; }

        public AutoCompleteResource Execute(AgsClient client, GeometryServiceResource parent)
        {
            string resourcePath = String.Format("{0}/autoComplete", parent.resourcePath);
            return (AutoCompleteResource)Execute(client, resourcePath);
        }
        public override BaseResponse Execute(AgsClient client, string resourcePath)
        {
            var request = createRequest(resourcePath);
            var result = client.Execute<AutoCompleteResource>(request, Method.POST);

            return result;
        }

        private RestRequest createRequest(string resourcePath)
        {
            var request = new RestRequest(resourcePath) { Method = Method.POST };

            var jss = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };

            if (polygons != null)
                request.AddParameter("polygons", JsonConvert.SerializeObject(polygons, jss));
            if (polylines != null)
                request.AddParameter("polylines", JsonConvert.SerializeObject(polylines, jss));
            if (sr != null)
                request.AddParameter("sr", sr.wkid);

            return request;
        }
    }
}
