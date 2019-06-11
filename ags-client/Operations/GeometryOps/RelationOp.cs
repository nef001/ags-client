using System;
using System.Collections.Generic;

using ags_client.Types.Geometry;
using RestSharp;
using Newtonsoft.Json;

namespace ags_client.Operations.GeometryOps
{
    public class RelationOp<TG1, TG2>
        where TG1 : IRestGeometry
        where TG2 : IRestGeometry
    {
        public Geometries<TG1> geometries1 { get; set; }
        public Geometries<TG2> geometries2 { get; set; }
        public SpatialReference sr { get; set; }
        public string relation { get; set; }
        public string relationParam { get; set; }

        public RelationOpResponse Execute(AgsClient client, string servicePath)
        {
            var request = new RestRequest(String.Format("{0}/{1}/{2}", servicePath, "GeometryServer", "relation"));
            request.Method = Method.POST;

            var jss = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };

            if (geometries1 != null)
                request.AddParameter("geometries1", JsonConvert.SerializeObject(geometries1, jss));
            if (geometries2 != null)
                request.AddParameter("geometries2", JsonConvert.SerializeObject(geometries2, jss));
            if (sr != null)
                request.AddParameter("sr", JsonConvert.SerializeObject(sr, jss));
            if (!String.IsNullOrWhiteSpace(relation))
                request.AddParameter("relation", relation);
            if (!String.IsNullOrWhiteSpace(relationParam))
                request.AddParameter("relationParam", relationParam);

            var result = client.Execute<RelationOpResponse>(request, Method.POST);

            return result;
        }
    }

    public class RelationOpResponse : BaseResponse
    {
        public List<GeometryRelation> relations { get; set; }
    }
}
