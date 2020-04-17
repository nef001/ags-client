using ags_client.Resources.Common;
using ags_client.Resources.FeatureService;
using ags_client.Types;
using ags_client.Types.Geometry;
using RestSharp;
using System;
using System.Threading.Tasks;

namespace ags_client.Requests.FeatureService
{
    public class FeatureServiceLayerFeatureRequest<TF, TG, TA> : BaseRequest
        where TF : IRestFeature<TG, TA>
        where TG : IRestGeometry
        where TA : IRestAttributes
    {
        private readonly int _objectId;

        public bool? returnZ { get; set; }
        public bool? returnM { get; set; }
        public string gdbVersion { get; set; }

        public FeatureServiceLayerFeatureRequest(int objectId)
        {
            _objectId = objectId;
        }

        public FeatureResource<TF, TG, TA> Execute(AgsClient client, FeatureServiceLayerResource<TA> parent)
        {
            string resourcePath = $"{parent.resourcePath}/{_objectId}";
            return (FeatureResource<TF, TG, TA>)Execute(client, resourcePath);
        }

        public async Task<FeatureResource<TF, TG, TA>> ExecuteAsync(AgsClient client, FeatureServiceLayerResource<TA> parent)
        {
            string resourcePath = $"{parent.resourcePath}/{_objectId}";
            var request = createRequest(resourcePath);

            return await client.ExecuteAsync<FeatureResource<TF, TG, TA>>(request, Method.POST);
        }

        public override BaseResponse Execute(AgsClient client, string resourcePath)
        {
            var request = createRequest(resourcePath);
            return client.Execute<FeatureResource<TF, TG, TA>>(request, Method.GET);
        }

        private RestRequest createRequest(string resourcePath)
        {
            var request = new RestRequest(resourcePath) { Method = Method.POST };

            if (returnZ.HasValue)
                request.AddParameter("returnZ", returnZ.Value ? "true" : "false");
            if (returnM.HasValue)
                request.AddParameter("returnM", returnM.Value ? "true" : "false");
            if (!String.IsNullOrWhiteSpace(gdbVersion))
                request.AddParameter("gdbVersion", gdbVersion);

            request.AddParameter("f", "json");

            return request;
        }
    }
}
