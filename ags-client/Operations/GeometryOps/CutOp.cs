using System;
using System.Collections.Generic;

using RestSharp;
using Newtonsoft.Json;

using ags_client.Types.Geometry;
using ags_client.JsonConverters;

namespace ags_client.Operations.GeometryOps
{
    public class CutOp<TG>
        where TG : IRestGeometry
    {
        public Polyline cutter { get; set; }
        public Geometries<TG> target { get; set; }
        public SpatialReference sr { get; set; }

        public CutResponse Execute(AgsClient client, string servicePath)
        {
            //servicePath is typically "Utilities/Geometry"

            var request = new RestRequest(String.Format("{0}/{1}/{2}", servicePath, "GeometryServer", "cut "));
            request.Method = Method.POST;

            var jss = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };

            if (cutter != null)
                request.AddParameter("cutter", JsonConvert.SerializeObject(cutter, jss));
            if (target != null)
                request.AddParameter("target", JsonConvert.SerializeObject(target, jss));
            if (sr != null)
                request.AddParameter("sr", sr.wkid);

            var result = client.Execute<CutResponse>(request, Method.POST);

            return result;
        }
    }

    [JsonConverter(typeof(CutResponseConverter))]
    public class CutResponse : BaseResponse
    {
        public string geometryType { get; set; }
        public List<IRestGeometry> geometries { get; set; }
        public List<int> cutIndexes { get; set; }
    }
}
