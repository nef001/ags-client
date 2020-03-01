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
    public class ApplyEditsRequest<TF, TG, TA> : BaseRequest
        where TF : IRestFeature<TG, TA>
        where TG : IRestGeometry
        where TA : IRestAttributes
    {
        public List<TF> adds { get; set; }
        public List<TF> updates { get; set; }
        public List<int> deletes { get; set; }
        public string gdbVersion { get; set; }
        public bool? rollbackOnFailure { get; set; }

        const string resource = "applyEdits";

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
            var request = new RestRequest(resourcePath, Method.POST);
            var jss = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Include,
                MissingMemberHandling = MissingMemberHandling.Ignore,
            };
            request.AddParameter("adds", JsonConvert.SerializeObject(adds, jss));
            request.AddParameter("updates", JsonConvert.SerializeObject(updates, jss));
            request.AddParameter("deletes", deletes == null ? "" : String.Join(",", deletes));
            request.AddParameter("gdbVersion", JsonConvert.SerializeObject(gdbVersion, jss));
            request.AddParameter("rollbackOnFailure", JsonConvert.SerializeObject(rollbackOnFailure, jss));
            request.AddParameter("f", "pjson");

            return request;
        }
    }
}
