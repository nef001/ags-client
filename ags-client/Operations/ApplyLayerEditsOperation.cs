using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RestSharp;
using Newtonsoft.Json;

using ags_client.Types.Geometry;
using ags_client.Types;

namespace ags_client.Operations
{
    public class ApplyLayerEditsOperation<TF, TG, TA>
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


        public EditFeaturesResponse Execute(AgsClient client, string servicePath, int layerId)
        {
            var request = new RestRequest(String.Format("{0}/{1}/{2}/applyEdits", servicePath, "FeatureServer", layerId));
            request.Method = Method.POST;

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


            var result = client.Execute<EditFeaturesResponse>(request, Method.POST);

            return result;
        }



    }
}
