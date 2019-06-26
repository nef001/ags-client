using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using ags_client.Resources.GeometryService;
using ags_client.Types.Geometry;

namespace ags_client.Requests.GeometryService
{
    public class LengthsRequest : BaseRequest
    {
        public List<Polyline> polylines { get; set; }
        public SpatialReference sr { get; set; }
        public int? lengthUnit { get; set; }
        public bool? geodesic { get; set; }
        public string calculationType { get; set; } //planar, geodesic or preserveShape

        public LengthsResource Execute(AgsClient client, GeometryServiceResource parent)
        {
            string resourcePath = String.Format("{0}/lengths", parent.resourcePath);
            return (LengthsResource)Execute(client, resourcePath);
        }
        public override BaseResponse Execute(AgsClient client, string resourcePath)
        {
            var request = new RestRequest(resourcePath) { Method = Method.POST };

            var jss = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };

            if (polylines != null)
                request.AddParameter("polylines", JsonConvert.SerializeObject(polylines, jss));

            if (sr != null)
                request.AddParameter("sr", JsonConvert.SerializeObject(sr, jss));

            if (lengthUnit.HasValue)
                request.AddParameter("lengthUnit", lengthUnit);

            if (geodesic.HasValue)
                request.AddParameter("geodesic", geodesic.Value ? "true" : "false");

            if (!String.IsNullOrWhiteSpace(calculationType))
                request.AddParameter("calculationType", calculationType);

            var result = client.Execute<LengthsResource>(request, Method.POST);

            return result;
        }
    }
}
