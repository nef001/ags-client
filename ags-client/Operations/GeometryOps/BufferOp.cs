﻿using System;
using System.Collections.Generic;
using System.Linq;

using RestSharp;
using Newtonsoft.Json;
using ags_client.Types.Geometry;

namespace ags_client.Operations.GeometryOps
{
    public class BufferOp<TG>
        where TG : IRestGeometry
    {
        public Geometries<TG> geometries { get; set; }
        public SpatialReference inSR { get; set; }
        public SpatialReference outSR { get; set; }
        public SpatialReference bufferSR { get; set; }
        public List<double> distances { get; set; }
        public int? unit { get; set; } /* See esriSRUnitType Constants and esriSRUnit2Type Constants */
        public bool? unionResults { get; set; }
        public bool? geodesic { get; set; }

        public BufferResponse<Polygon> Execute(AgsClient client, string servicePath)
        {
            //nb Polygon is always the return type of a buffer op

            //servicePath is typically "Utilities/Geometry"

            var request = new RestRequest(String.Format("{0}/{1}/{2}", servicePath, "GeometryServer", "buffer"));
            request.Method = Method.POST;

            var jss = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };

            if (geometries != null)
                request.AddParameter("geometries", JsonConvert.SerializeObject(geometries, jss));
            if (inSR != null)
                request.AddParameter("inSR", inSR.wkid);
            if (outSR != null)
                request.AddParameter("outSR", outSR.wkid);
            if (bufferSR != null)
                request.AddParameter("bufferSR", bufferSR.wkid);
            if ((distances != null) && (distances.Count > 0))
                request.AddParameter("distances", String.Join(",", distances.Select(n => n.ToString()).ToArray()));
            if (unit.HasValue)
                request.AddParameter("unit", unit);
            if (unionResults.HasValue)
                request.AddParameter("unionResults", unionResults.Value ? "true" : "false");
            if (geodesic.HasValue)
                request.AddParameter("geodesic", geodesic.Value ? "true" : "false");

            var result = client.Execute<BufferResponse<Polygon>>(request, Method.POST);

            return result;
        }
    }
}
