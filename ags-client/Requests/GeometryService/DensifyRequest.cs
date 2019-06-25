using System;
using RestSharp;
using Newtonsoft.Json;
using ags_client.Resources.GeometryService;
using ags_client.Types.Geometry;

namespace ags_client.Requests.GeometryService
{
    public class DensifyRequest<TG>:BaseRequest
        where TG : IRestGeometry
    {
        public Geometries<TG> geometries { get; set; } //polyline or polygon
        public SpatialReference sr { get; set; }
        public double? maxSegmentLength { get; set; }
        public bool? geodesic { get; set; }
        public int? lengthUnit { get; set; }

        public DensifyResource<TG> Execute(AgsClient client, GeometryServiceResource parent)
        {
            string resourcePath = String.Format("{0}/densify", parent.resourcePath);
            return (DensifyResource<TG>)Execute(client, resourcePath);
        }
        public override BaseResponse Execute(AgsClient client, string resourcePath)
        {
            var request = new RestRequest(resourcePath) { Method = Method.POST };

            var jss = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };

            if (geometries != null)
                request.AddParameter("geometries", JsonConvert.SerializeObject(geometries, jss));
            if (sr != null)
                request.AddParameter("sr", JsonConvert.SerializeObject(sr, jss));
            if (maxSegmentLength.HasValue)
                request.AddParameter("maxSegmentLength", maxSegmentLength);
            if (geodesic.HasValue)
                request.AddParameter("geodesic", geodesic.Value ? "true" : "false");
            if (lengthUnit.HasValue)
                request.AddParameter("lengthUnit", lengthUnit);

            var result = client.Execute<DensifyResource<TG>>(request, Method.POST);

            return result;
        }
    }
}
