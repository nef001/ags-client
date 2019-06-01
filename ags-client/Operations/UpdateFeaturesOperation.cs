using System;
using System.Collections.Generic;
using System.Text;

using RestSharp;
using Newtonsoft.Json;

using ags_client.Types;
using ags_client.Types.Geometry;
using ags_client.Utility;

namespace ags_client.Operations
{
    public class UpdateFeaturesOperation<TF, TG, TA>
        where TF : IRestFeature<TG, TA>
        where TG : IRestGeometry
        where TA : IRestAttributes
    {
        public List<TF> features { get; set; }
        public string gdbVersion { get; set; }
        public bool? rollbackOnFailure { get; set; }

        public EditFeaturesResponse Execute2(AgsClient client, string servicePath, int layerId)
        {
            //using .net WebRequest in place of restsharp

            string url = String.Format("{0}/{1}/{2}/{3}/updateFeatures", client.BaseUrl, servicePath, "FeatureServer", layerId);

            StringBuilder json = new StringBuilder("f=pjson");
            if (!String.IsNullOrEmpty(gdbVersion)) json.AppendFormat("&gdbVersion={0}", gdbVersion);
            if (rollbackOnFailure.HasValue) json.AppendFormat("&rollbackOnFailure={0}", rollbackOnFailure);
            json.AppendFormat("&features={0}", JsonConvert.SerializeObject(features));

            string jsonData = json.ToString();

            string responseString = HttpUtil.GetResponse(url, jsonData);

            var result = JsonConvert.DeserializeObject<EditFeaturesResponse>(responseString, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            return result;
        }

        public EditFeaturesResponse Execute(AgsClient client, string servicePath, int layerId)
        {
            var request = new RestRequest(String.Format("{0}/{1}/{2}/updateFeatures", servicePath, "FeatureServer", layerId));
            request.Method = Method.POST;

            if (rollbackOnFailure.HasValue)
                request.AddParameter("rollbackOnFailure", rollbackOnFailure.ToString());
            if (!String.IsNullOrEmpty(gdbVersion))
                request.AddParameter("gdbVersion", gdbVersion);
            if (features != null)
            {
                string jsonFeatures = JsonConvert.SerializeObject(features);
                var length = jsonFeatures.Length;
                Console.WriteLine("jsonFeatures length: {0}", length);

                request.AddParameter("features", jsonFeatures);
            }

            var result = client.Execute<EditFeaturesResponse>(request, Method.POST);

            return result;
        }

    }
}
