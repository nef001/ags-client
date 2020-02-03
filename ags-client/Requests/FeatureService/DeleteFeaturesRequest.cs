using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using RestSharp;
using Newtonsoft.Json;
using ags_client.Resources.FeatureService;
using ags_client.Types;
using ags_client.Types.Geometry;

namespace ags_client.Requests.FeatureService
{
    public class DeleteFeaturesRequest<TA> : BaseRequest
        where TA : IRestAttributes
    {
        public List<int> objectIds { get; set; }
        public string where { get; set; }
        public IRestGeometry geometry { get; set; }
        public string geometryType { get; set; }
        public SpatialReference inSR { get; set; }
        public string spatialRel { get; set; }
        public string gdbVersion { get; set; }
        public bool? rollbackOnFailure { get; set; }

        const string resource = "deleteFeatures";

        public EditFeaturesResource Execute(AgsClient client, FeatureServiceLayerResource<TA> parent)
        {
            string resourcePath = $"{parent.resourcePath}/{resource}";
            return (EditFeaturesResource)Execute(client, resourcePath);
        }

        public async Task<EditFeaturesResource> ExecuteAsync(AgsClient client, FeatureServiceLayerResource<TA> parent)
        {
            string resourcePath = $"{parent.resourcePath}/{resource}";
            var request = createRequest(resourcePath);

            return await client.ExecuteAsync<EditFeaturesResource>(request, Method.POST);
        }

        public override BaseResponse Execute(AgsClient client, string resourcePath)
        {
            var request = createRequest(resourcePath);
            return client.Execute<EditFeaturesResource>(request, Method.POST);
        }

        private RestRequest createRequest(string resourcePath)
        {
            var request = new RestRequest(resourcePath) { Method = Method.POST };

            var jss = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };

            if ((objectIds != null) && (objectIds.Count > 0))
                request.AddParameter("objectIds", String.Join(",", objectIds));
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

            request.AddParameter("f", "json");

            return request;
        }
    }
}
