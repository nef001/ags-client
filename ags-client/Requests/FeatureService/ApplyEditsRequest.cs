using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RestSharp;
using Newtonsoft.Json;
using ags_client.Resources.FeatureService;
using ags_client.Types;
using ags_client.Types.Geometry;

namespace ags_client.Requests.FeatureService
{
    public class ApplyEditsRequest<TF, TG, TA> : BaseRequest
        where TF : IRestFeature<TG, TA>
        where TG : IRestGeometry
        where TA : IRestAttributes
    {
        public List<TF> adds { get; set; }
        public List<TF> updates { get; set; }
        public List<string> deletes { get; set; }
        public string gdbVersion { get; set; }
        public bool? rollbackOnFailure { get; set; }
        public bool? useGlobalIds { get; set; }

        public EditFeaturesResource Execute(AgsClient client, FeatureServiceLayerResource<TA> parent)
        {
            string resourcePath = String.Format("{0}/applyEdits", parent.resourcePath);
            return (EditFeaturesResource)Execute(client, resourcePath);
        }

        public override BaseResponse Execute(AgsClient client, string resourcePath)
        {
            var request = new RestRequest(resourcePath) { Method = Method.POST };

            var jss = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };

            if ((adds != null) && (adds.Count > 0))
                request.AddParameter("adds", JsonConvert.SerializeObject(adds, jss));
            if ((updates != null) && (updates.Count > 0))
                request.AddParameter("updates", JsonConvert.SerializeObject(updates, jss));
            if ((deletes != null) && (deletes.Count > 0))
                request.AddParameter("deletes", JsonConvert.SerializeObject(deletes, jss));

            if (!String.IsNullOrEmpty(gdbVersion))
                request.AddParameter("gdbVersion", gdbVersion);
            if (rollbackOnFailure.HasValue)
                request.AddParameter("rollbackOnFailure", rollbackOnFailure.Value ? "true" : "false");
            if (useGlobalIds.HasValue)
                request.AddParameter("useGlobalIds", useGlobalIds.Value ? "true" : "false");

            var result = client.Execute<EditFeaturesResource>(request, Method.POST);

            return result;
        }
    }
}
