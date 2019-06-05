using System;
using System.Collections.Generic;

using RestSharp;
using Newtonsoft.Json;

using ags_client.Types.Geometry;

namespace ags_client.Operations.GeometryOps
{
    public class ProjectOp
    {
        public string geometryType { get; set; }
        public List<IRestGeometry> geometries { get; set; }
        public SpatialReference inSR { get; set; }
        public SpatialReference outSR { get; set; }
        public Transformation transformation { get; set; }
        public bool? transformForward { get; set; }

        public GeometriesResponse Execute(AgsClient client, string servicePath)
        {
            //servicePath is typically "Utilities/Geometry"

            var request = new RestRequest(String.Format("{0}/{1}/{2}", servicePath, "GeometryServer", "project"));
            request.Method = Method.POST;

            var jss = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };

            if (!String.IsNullOrEmpty(geometryType))
                request.AddParameter("geometryType", geometryType);
            if ((geometries != null) && (geometries.Count > 0))
                request.AddParameter("geometries", JsonConvert.SerializeObject(geometries, jss));
            if (inSR != null)
                request.AddParameter("inSR", JsonConvert.SerializeObject(inSR, jss));
            if (outSR != null)
                request.AddParameter("outSR", JsonConvert.SerializeObject(outSR, jss));
            if (transformation != null)
                request.AddParameter("transformation", JsonConvert.SerializeObject(transformation, jss));
            if (transformForward.HasValue)
                request.AddParameter("transformForward", transformForward.Value ? "true" : "false");

            var result = client.Execute<GeometriesResponse>(request, Method.POST);

            return result;
        }
    }
}
