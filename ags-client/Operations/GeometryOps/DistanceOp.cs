using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RestSharp;
using Newtonsoft.Json;

using ags_client.Types.Geometry;

namespace ags_client.Operations.GeometryOps
{
    public class DistanceOp
    {
        public IRestGeometry geometry1 { get; set; }
        public IRestGeometry geometry2 { get; set; }
        public SpatialReference sr { get; set; }
        public string distanceUnit { get; set; }
        public bool? geodesic { get; set; }

        public DistanceResponse Execute(AgsClient client, string servicePath)
        {
            //servicePath is typically "Utilities/Geometry"

            var request = new RestRequest(String.Format("{0}/{1}/{2}", servicePath, "GeometryServer", "distance"));
            request.Method = Method.POST;

            var jss = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };

            if (geometry1 != null)
                request.AddParameter("geometry1", JsonConvert.SerializeObject(geometry1, jss));
            if (geometry2 != null)
                request.AddParameter("geometry2", JsonConvert.SerializeObject(geometry2, jss));
            if (sr != null)
                request.AddParameter("sr", JsonConvert.SerializeObject(sr, jss));
            if (!String.IsNullOrEmpty(distanceUnit))
                request.AddParameter("distanceUnit", distanceUnit);
            if (geodesic.HasValue)
                request.AddParameter("geodesic", geodesic.Value ? "true" : "false");

            var result = client.Execute<DistanceResponse>(request, Method.POST);

            return result;
        }
    }
}
