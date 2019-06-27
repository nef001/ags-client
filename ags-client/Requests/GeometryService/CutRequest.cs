using System;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using ags_client.Resources.GeometryService;
using ags_client.Types.Geometry;

namespace ags_client.Requests.GeometryService
{
    public class CutRequest<TG> : BaseRequest
        where TG : IRestGeometry
    {
        public Polyline cutter { get; set; }
        public Geometries<TG> target { get; set; }
        public SpatialReference sr { get; set; }

        const string resource = "cut";

        public CutResource Execute(AgsClient client, GeometryServiceResource parent)
        {
            string resourcePath = String.Format("{0}/{1}", parent.resourcePath, resource);
            return (CutResource)Execute(client, resourcePath);
        }

        public async Task<CutResource> ExecuteAsync(AgsClient client, GeometryServiceResource parent)
        {
            string resourcePath = String.Format("{0}/{1}", parent.resourcePath, resource);
            var request = createRequest(resourcePath);

            return await client.ExecuteAsync<CutResource>(request, Method.POST);
        }

        public override BaseResponse Execute(AgsClient client, string resourcePath)
        {
            var request = createRequest(resourcePath);
            var result = client.Execute<CutResource>(request, Method.POST);

            return result;
        }

        private RestRequest createRequest(string resourcePath)
        {
            var request = new RestRequest(resourcePath) { Method = Method.POST };

            var jss = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };

            if (cutter != null)
                request.AddParameter("cutter", JsonConvert.SerializeObject(cutter, jss));
            if (target != null)
                request.AddParameter("target", JsonConvert.SerializeObject(target, jss));
            if (sr != null)
                request.AddParameter("sr", sr.wkid);

            return request;
        }
    }
}
