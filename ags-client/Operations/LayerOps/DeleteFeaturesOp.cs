using System;
using System.Collections.Generic;

using RestSharp;
using Newtonsoft.Json;

using ags_client.Types.Geometry;

namespace ags_client.Operations.LayerOps
{
    public class DeleteFeaturesOp
    {
        public List<int> objectids { get; set; }
        public string where { get; set; }
        public IRestGeometry geometry { get; set; }
        public string geometryType { get; set; }
        public SpatialReference inSR { get; set; }
        public string spatialRel { get; set; }
        public string gdbVersion { get; set; }
        public bool? rollbackOnFailure { get; set; }

        public EditFeaturesResponse Execute(AgsClient client, string servicePath, int layerId)
        {
            var request = new RestRequest(String.Format("{0}/{1}/{2}/deleteFeatures", servicePath, "FeatureServer", layerId));
            request.Method = Method.POST;

            var jss = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };

            if ((objectids != null) && (objectids.Count > 0))
                request.AddParameter("objectids", JsonConvert.SerializeObject(objectids, jss));
            if (!String.IsNullOrEmpty(where))
                request.AddParameter("where", where);
            if (geometry != null)
                request.AddParameter("geometry", JsonConvert.SerializeObject(geometry, jss));
            if (!String.IsNullOrEmpty(geometryType))
                request.AddParameter("geometryType", geometryType);
            if (inSR != null)
                request.AddParameter("inSR", JsonConvert.SerializeObject(inSR, jss));
            if (!String.IsNullOrEmpty(spatialRel))
                request.AddParameter("spatialRel", spatialRel);
            if (!String.IsNullOrEmpty(gdbVersion))
                request.AddParameter("gdbVersion", gdbVersion);
            if (rollbackOnFailure.HasValue)
                request.AddParameter("rollbackOnFailure", rollbackOnFailure.Value ? "true" : "false");

            var result = client.Execute<EditFeaturesResponse>(request, Method.POST);

            return result;
        }
    }
}
