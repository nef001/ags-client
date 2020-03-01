using ags_client.Resources.FeatureService;
using ags_client.Types;
using ags_client.Types.Geometry;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ags_client.Requests.FeatureService
{
    public class UpdateFeaturesRequest<TF, TG, TA> : BaseRequest
        where TF : IRestFeature<TG, TA>
        where TG : IRestGeometry
        where TA : IRestAttributes
    {
        public List<TF> features { get; set; }
        public string gdbVersion { get; set; }
        public bool? rollbackOnFailure { get; set; }

        const string resource = "updateFeatures";

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

            if (rollbackOnFailure.HasValue)
                request.AddParameter("rollbackOnFailure", rollbackOnFailure.Value ? "true" : "false");
            if (!String.IsNullOrEmpty(gdbVersion))
                request.AddParameter("gdbVersion", gdbVersion);
            if ((features != null) && (features.Count > 0))
                request.AddParameter("features", JsonConvert.SerializeObject(features, jss));

            request.AddParameter("f", "json");

            return request;
        }
    }
}
