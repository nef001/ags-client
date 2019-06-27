using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using RestSharp;
using Newtonsoft.Json;
using ags_client.Resources.GeometryService;
using ags_client.Types.Geometry;

namespace ags_client.Requests.GeometryService
{
    public class TrimExtendRequest : BaseRequest
    {
        public List<Polyline> polylines { get; set; }
        public Polyline trimExtendTo { get; set; }
        public SpatialReference sr { get; set; }
        public int? extendHow { get; set; }

        const string resource = "trimExtend";

        public TrimExtendResource Execute(AgsClient client, GeometryServiceResource parent)
        {
            string resourcePath = String.Format("{0}/{1}", parent.resourcePath, resource);
            return (TrimExtendResource)Execute(client, resourcePath);
        }

        public async Task<TrimExtendResource> ExecuteAsync(AgsClient client, GeometryServiceResource parent)
        {
            string resourcePath = String.Format("{0}/{1}", parent.resourcePath, resource);
            var request = createRequest(resourcePath);

            return await client.ExecuteAsync<TrimExtendResource>(request, Method.POST);
        }

        public override BaseResponse Execute(AgsClient client, string resourcePath)
        {
            var request = createRequest(resourcePath);
            var result = client.Execute<TrimExtendResource>(request, Method.POST);

            return result;
        }

        private RestRequest createRequest(string resourcePath)
        {
            var request = new RestRequest(resourcePath) { Method = Method.POST };

            var jss = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };

            if (polylines != null)
                request.AddParameter("polylines", JsonConvert.SerializeObject(polylines, jss));
            if (trimExtendTo != null)
                request.AddParameter("trimExtendTo", JsonConvert.SerializeObject(trimExtendTo, jss));
            if (sr != null)
                request.AddParameter("sr", JsonConvert.SerializeObject(sr, jss));
            if (extendHow.HasValue)
                request.AddParameter("extendHow", extendHow);

            return request;
        }
    }
}
