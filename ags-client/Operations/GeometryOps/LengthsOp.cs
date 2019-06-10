using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ags_client.Types.Geometry;
using RestSharp;
using Newtonsoft.Json;

namespace ags_client.Operations.GeometryOps
{
    public class LengthsOp
    {
        public List<Polyline> polylines { get; set; }
        public SpatialReference sr { get; set; }
        public int? lengthUnit { get; set; }
        public bool? geodesic { get; set; }
        public string calculationType { get; set; } //planar, geodesic or preserveShape

        public LengthsOpResponse Execute(AgsClient client, string servicePath)
        {
            var request = new RestRequest(String.Format("{0}/{1}/{2}", servicePath, "GeometryServer", "lengths"));
            request.Method = Method.POST;

            var jss = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };

            if (polylines != null)
                request.AddParameter("polygons", JsonConvert.SerializeObject(polylines, jss));

            if (sr != null)
                request.AddParameter("sr", JsonConvert.SerializeObject(sr, jss));

            if (lengthUnit.HasValue)
                request.AddParameter("lengthUnit", lengthUnit);

            if (geodesic.HasValue)
                request.AddParameter("geodesic", geodesic.Value ? "true" : "false");

            if (!String.IsNullOrWhiteSpace(calculationType))
                request.AddParameter("calculationType", calculationType);

            var result = client.Execute<LengthsOpResponse>(request, Method.POST);

            return result;
        }
    }

    public class LengthsOpResponse : BaseResponse
    {
        public List<double> lengths { get; set; }
    }
}
