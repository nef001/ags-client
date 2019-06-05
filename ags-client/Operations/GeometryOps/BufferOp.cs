using System;
using System.Collections.Generic;

using RestSharp;
using Newtonsoft.Json;
using ags_client.Types.Geometry;

namespace ags_client.Operations.GeometryOps
{
    public class BufferOp
    {
        public string geometryType { get; set; }
        public List<IRestGeometry> geometries { get; set; }
        public SpatialReference inSR { get; set; }
        public SpatialReference outSR { get; set; }
        public SpatialReference bufferSR { get; set; }
        public List<double> distances { get; set; }
        public string unit { get; set; }
        public bool? unionResults { get; set; }
        public bool? geodesic { get; set; }

        public GeometriesResponse Execute(AgsClient client, string servicePath)
        {
            //servicePath is typically "Utilities/Geometry"

            var request = new RestRequest(String.Format("{0}/{1}/{2}", servicePath, "GeometryServer", "buffer"));
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
            if (bufferSR != null)
                request.AddParameter("bufferSR", JsonConvert.SerializeObject(bufferSR, jss));
            if ((distances != null) && (distances.Count > 0))
                request.AddParameter("distances", JsonConvert.SerializeObject(distances, jss));
            if (!String.IsNullOrEmpty(unit))
                request.AddParameter("unit", unit);
            if (unionResults.HasValue)
                request.AddParameter("unionResults", unionResults.Value ? "true" : "false");
            if (geodesic.HasValue)
                request.AddParameter("geodesic", geodesic.Value ? "true" : "false");

            var result = client.Execute<GeometriesResponse>(request, Method.POST);

            return result;
        }
    }
}
