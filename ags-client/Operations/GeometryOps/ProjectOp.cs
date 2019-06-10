using System;
using System.Collections.Generic;

using RestSharp;
using Newtonsoft.Json;

using ags_client.Types.Geometry;

namespace ags_client.Operations.GeometryOps
{
    public class ProjectOp<TG>
        where TG : IRestGeometry
    {
        public Geometries<TG> geometries { get; set; }
        public SpatialReference inSR { get; set; }
        public SpatialReference outSR { get; set; }
        public Transformation transformation { get; set; }
        public bool? transformForward { get; set; }

        public ProjectResponse<TG> Execute(AgsClient client, string servicePath)
        {
            //servicePath is typically "Utilities/Geometry"

            var request = new RestRequest(String.Format("{0}/{1}/{2}", servicePath, "GeometryServer", "project"));
            request.Method = Method.POST;

            var jss = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };

            if (geometries != null)
                request.AddParameter("geometries", JsonConvert.SerializeObject(geometries, jss));
            if (inSR != null)
                request.AddParameter("inSR", inSR.wkid);
            if (outSR != null)
                request.AddParameter("outSR", outSR.wkid);
            if (transformation != null)
                request.AddParameter("transformation", JsonConvert.SerializeObject(transformation, jss));
            if (transformForward.HasValue)
                request.AddParameter("transformForward", transformForward.Value ? "true" : "false");

            var result = client.Execute<ProjectResponse<TG>>(request, Method.POST);

            return result;
        }
    }

    public class ProjectResponse<TG> : BaseResponse
        where TG : IRestGeometry
    {
        public List<TG> geometries { get; set; }
    }
}
