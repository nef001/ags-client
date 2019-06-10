﻿using System;

using RestSharp;
using Newtonsoft.Json;

using ags_client.Types.Geometry;

namespace ags_client.Operations.GeometryOps
{
    public class DistanceOp<TG1, TG2>
        where TG1 : IRestGeometry
        where TG2 : IRestGeometry
    {
        public Geometry<TG1> geometry1 { get; set; }
        public Geometry<TG2> geometry2 { get; set; }
        public SpatialReference sr { get; set; }
        public int? distanceUnit { get; set; }
        public bool? geodesic { get; set; }

        public DistanceResponse Execute(AgsClient client, string servicePath)
        {
            //servicePath is typically "Utilities/Geometry"

            var request = new RestRequest(String.Format("{0}/{1}/{2}", servicePath, "GeometryServer", "distance"));
            request.Method = Method.POST;

            var jss = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };

            if (geometry1 != null)
                request.AddParameter("geometry1", JsonConvert.SerializeObject(geometry1, jss));
            if (geometry2 != null)
                request.AddParameter("geometry2", JsonConvert.SerializeObject(geometry2, jss));
            if (sr != null)
                request.AddParameter("sr", JsonConvert.SerializeObject(sr, jss));
            if (distanceUnit.HasValue)
                request.AddParameter("distanceUnit", distanceUnit);
            if (geodesic.HasValue)
                request.AddParameter("geodesic", geodesic.Value ? "true" : "false");

            var result = client.Execute<DistanceResponse>(request, Method.POST);

            return result;
        }
    }

    public class DistanceResponse : BaseResponse
    {
        public double distance { get; set; }
    }
}
