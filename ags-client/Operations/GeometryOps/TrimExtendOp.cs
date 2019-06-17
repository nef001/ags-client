using System;
using System.Collections.Generic;

using ags_client.Types.Geometry;
using RestSharp;
using Newtonsoft.Json;

namespace ags_client.Operations.GeometryOps
{
    public class TrimExtendOp
    {
        public List<Polyline> polylines { get; set; }
        public Polyline trimExtendTo { get; set; }
        public SpatialReference sr { get; set; }
        public int? extendHow { get; set; }

        public TrimExtendOpResponse Execute(AgsClient client, string servicePath)
        {
            var request = new RestRequest(String.Format("{0}/{1}/{2}", servicePath, "GeometryServer", "trimExtend"));
            request.Method = Method.POST;

            var jss = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };

            if (polylines != null)
                request.AddParameter("polylines", JsonConvert.SerializeObject(polylines, jss));

            if (trimExtendTo != null)
                request.AddParameter("trimExtendTo", JsonConvert.SerializeObject(trimExtendTo, jss));

            if (sr != null)
                request.AddParameter("sr", JsonConvert.SerializeObject(sr, jss));

            if (extendHow.HasValue)
                request.AddParameter("extendHow", extendHow);

            var result = client.Execute<TrimExtendOpResponse>(request, Method.POST);

            return result;
        }
    }

    public class TrimExtendOpResponse : BaseResponse
    {
        public string geometryType { get; set; }
        public List<Polyline> geometries { get; set; }
    }
}
