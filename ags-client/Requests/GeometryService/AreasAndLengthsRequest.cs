using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using RestSharp;
using Newtonsoft.Json;
using ags_client.Resources.GeometryService;
using ags_client.Types.Geometry;

namespace ags_client.Requests.GeometryService
{
    public class AreasAndLengthsRequest : BaseRequest
    {
        public List<Polygon> polygons { get; set; }
        public SpatialReference sr { get; set; }
        public string lengthUnit { get; set; }
        public string areaUnit { get; set; }
        public string calculationType { get; set; }

        public AreasAndLengthsResource Execute(AgsClient client, GeometryServiceResource parent)
        {
            string resourcePath = String.Format("{0}/areasAndLengths", parent.resourcePath);
            return (AreasAndLengthsResource)Execute(client, resourcePath);
        }
        public override BaseResponse Execute(AgsClient client, string resourcePath)
        {
            var request = createRequest(resourcePath);
            var result = client.Execute<AreasAndLengthsResource>(request, Method.POST);

            return result;
        }

        private RestRequest createRequest(string resourcePath)
        {
            var request = new RestRequest(resourcePath) { Method = Method.POST };

            var jss = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };

            if (polygons != null)
                request.AddParameter("polygons", JsonConvert.SerializeObject(polygons, jss));
            if (sr != null)
                request.AddParameter("sr", sr.wkid);
            if (!String.IsNullOrWhiteSpace(lengthUnit))
                request.AddParameter("lengthUnit", lengthUnit);
            if (!String.IsNullOrWhiteSpace(areaUnit))
                request.AddParameter("areaUnit", areaUnit);
            if (!String.IsNullOrWhiteSpace(calculationType))
                request.AddParameter("calculationType", calculationType);

            return request;
        }
    }
}
