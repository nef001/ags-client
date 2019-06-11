using System;
using System.Collections.Generic;

using ags_client.Types.Geometry;
using RestSharp;
using Newtonsoft.Json;

namespace ags_client.Operations.GeometryOps
{
    public class SimplifyOp<TG>
        where TG:IRestGeometry
    {
        public Geometries<TG> geometries { get; set; }
        public SpatialReference sr { get; set; }

        public SimplifyOpResponse<TG> Execute(AgsClient client, string servicePath)
        {
            var request = new RestRequest(String.Format("{0}/{1}/{2}", servicePath, "GeometryServer", "simplify"));
            request.Method = Method.POST;

            var jss = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };

            if (geometries != null)
                request.AddParameter("geometries", JsonConvert.SerializeObject(geometries, jss));
            if (sr != null)
                request.AddParameter("sr", JsonConvert.SerializeObject(sr, jss));

            var result = client.Execute<SimplifyOpResponse<TG>>(request, Method.POST);

            return result;
        }
    }

    public class SimplifyOpResponse<TG> : BaseResponse
        where TG : IRestGeometry
    {
        public List<TG> geometries { get; set; }
    }
}
