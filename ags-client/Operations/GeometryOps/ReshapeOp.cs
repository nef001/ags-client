using System;
using System.Collections.Generic;

using ags_client.Types.Geometry;
using RestSharp;
using Newtonsoft.Json;

namespace ags_client.Operations.GeometryOps
{
    public class ReshapeOp<TG>
        where TG : IRestGeometry
    {
        public Geometry<TG> target { get; set; }
        public Polyline reshaper { get; set; }
        public SpatialReference sr { get; set; }

        public ReshapeOpResponse<TG> Execute(AgsClient client, string servicePath)
        {
            var request = new RestRequest(String.Format("{0}/{1}/{2}", servicePath, "GeometryServer", "reshape"));
            request.Method = Method.POST;

            var jss = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };

            if (target != null)
                request.AddParameter("target", JsonConvert.SerializeObject(target, jss));
            if (reshaper != null)
                request.AddParameter("reshaper", JsonConvert.SerializeObject(reshaper, jss));
            if (sr != null)
                request.AddParameter("sr", JsonConvert.SerializeObject(sr, jss));

            var result = client.Execute<ReshapeOpResponse<TG>>(request, Method.POST);

            return result;
        }
    }

    public class ReshapeOpResponse<TG> : BaseResponse
        where TG : IRestGeometry
    {
        public string geometryType { get; set; }
        public List<TG> geometries { get; set; }
    }

}
