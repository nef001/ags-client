using System;
using RestSharp;
using Newtonsoft.Json;
using ags_client.Resources.GeometryService;
using ags_client.Types.Geometry;


namespace ags_client.Requests.GeometryService
{
    public class OffsetRequest<TG> : BaseRequest
        where TG : IRestGeometry
    {
        public Geometries<TG> geometries { get; set; }
        public SpatialReference sr { get; set; }
        public double? offsetDistance { get; set; }
        public int? offsetUnit { get; set; }
        public string offsetHow { get; set; } //esriGeometryOffsetRounded, esriGeometryOffsetBevelled or esriGeometryOffsetMitered
        public double? bevelRatio { get; set; }
        public bool? simplifyResult { get; set; }

        public OffsetResource<TG> Execute(AgsClient client, GeometryServiceResource parent)
        {
            string resourcePath = String.Format("{0}/offset", parent.resourcePath);
            return (OffsetResource<TG>)Execute(client, resourcePath);
        }
        public override BaseResponse Execute(AgsClient client, string resourcePath)
        {
            var request = new RestRequest(resourcePath) { Method = Method.POST };

            var jss = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };

            if (geometries != null)
                request.AddParameter("geometries", JsonConvert.SerializeObject(geometries, jss));

            if (sr != null)
                request.AddParameter("sr", JsonConvert.SerializeObject(sr, jss));

            if (offsetDistance.HasValue)
                request.AddParameter("offsetDistance", offsetDistance);

            if (offsetUnit.HasValue)
                request.AddParameter("offsetUnit", offsetUnit);

            if (!String.IsNullOrWhiteSpace(offsetHow))
                request.AddParameter("offsetHow", offsetHow);

            if (bevelRatio.HasValue)
                request.AddParameter("bevelRatio", bevelRatio);

            if (simplifyResult.HasValue)
                request.AddParameter("simplifyResult", simplifyResult.Value ? "true" : "false");

            var result = client.Execute<OffsetResource<TG>>(request, Method.POST);

            return result;
        }
    }
}
