using System;
using System.Collections.Generic;

using ags_client.Types.Geometry;
using RestSharp;
using Newtonsoft.Json;

namespace ags_client.Operations.GeometryOps
{
    public class OffsetOp<TG>
        where TG : IRestGeometry
    {
        public Geometries<TG> geometries { get; set; }
        public SpatialReference sr { get; set; }
        public double? offsetDistance { get; set; }
        public int? offsetUnit { get; set; }
        public string offsetHow { get; set; } //esriGeometryOffsetRounded, esriGeometryOffsetBevelled or esriGeometryOffsetMitered
        public double? bevelRatio { get; set; }
        public bool? simplifyResult { get; set; }

        public OffsetOpResponse<TG> Execute(AgsClient client, string servicePath)
        {
            var request = new RestRequest(String.Format("{0}/{1}/{2}", servicePath, "GeometryServer", "offset"));
            request.Method = Method.POST;

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

            var result = client.Execute<OffsetOpResponse<TG>>(request, Method.POST);

            return result;
        }
    }

    public class OffsetOpResponse<TG> : BaseResponse
        where TG: IRestGeometry
    {
        public string geometryType { get; set; }
        public List<TG> geometries { get; set; }
    }
}
