using System;

using RestSharp;
using Newtonsoft.Json;

using ags_client.Types.Geometry;
using ags_client.JsonConverters;


namespace ags_client.Operations.GeometryOps
{
    public class ConvexHullOp<TG>
        where TG : IRestGeometry
    {
        public Geometries<TG> geometries { get; set; }
        public SpatialReference sr { get; set; }

        public ConvexHullResponse Execute(AgsClient client, string servicePath)
        {
            //servicePath is typically "Utilities/Geometry"

            var request = new RestRequest(String.Format("{0}/{1}/{2}", servicePath, "GeometryServer", "convexHull"));
            request.Method = Method.POST;

            var jss = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };

            if (geometries != null)
                request.AddParameter("geometries", JsonConvert.SerializeObject(geometries, jss));
            if (sr != null)
                request.AddParameter("sr", sr.wkid);

            var result = client.Execute<ConvexHullResponse>(request, Method.POST);

            return result;
        }
    }

    /* The convex hull is typically a polygon but can also be a polyline or point in degenerate cases.
     * So we use a custom converter to determine the geometry object to instantiate when deserializing. */
    [JsonConverter(typeof(ConvexHullResponseConverter))]
    public class ConvexHullResponse : BaseResponse
    {
        public string geometryType { get; set; }
        public IRestGeometry geometry { get; set; }

    }
}
