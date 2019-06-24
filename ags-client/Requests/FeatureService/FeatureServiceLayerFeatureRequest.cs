using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RestSharp;
using ags_client.Resources.FeatureService;
using ags_client.Resources.Common;
using ags_client.Types;
using ags_client.Types.Geometry;

namespace ags_client.Requests.FeatureService
{
    public class FeatureServiceLayerFeatureRequest<TF, TG, TA> : BaseRequest
        where TF : IRestFeature<TG, TA>
        where TG : IRestGeometry
        where TA : IRestAttributes
    {
        private int _objectId;

        public bool? returnZ { get; set; }
        public bool? returnM { get; set; }
        public string gdbVersion { get; set; }

        public FeatureServiceLayerFeatureRequest(int objectId)
        {
            _objectId = objectId;
        }

        public FeatureResource<TF, TG, TA> Execute(AgsClient client, FeatureServiceLayerResource<TA> parent)
        {
            string resourcePath = String.Format("{0}/{1}", parent.resourcePath, _objectId);
            return (FeatureResource < TF, TG, TA > )Execute(client, resourcePath);
        }

        public override BaseResponse Execute(AgsClient client, string resourcePath)
        {
            var request = new RestRequest(resourcePath) { Method = Method.GET };

            if (returnZ.HasValue)
                request.AddParameter("returnZ", returnZ.Value ? "true" : "false");
            if (returnM.HasValue)
                request.AddParameter("returnM", returnM.Value ? "true" : "false");
            if (!String.IsNullOrWhiteSpace(gdbVersion))
                request.AddParameter("gdbVersion", gdbVersion);

            var result = client.Execute<FeatureResource<TF, TG, TA>>(request, Method.GET);
            result.resourcePath = resourcePath;

            return result;
        }
    }
}
