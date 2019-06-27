using System;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using ags_client.Resources.GeometryService;
using ags_client.Types.Geometry;

namespace ags_client.Requests.GeometryService
{
    public class ReshapeRequest<TG> : BaseRequest
        where TG : IRestGeometry
    {
        public Geometry<TG> target { get; set; }
        public Polyline reshaper { get; set; }
        public SpatialReference sr { get; set; }

        const string resource = "reshape";

        public ReshapeResource<TG> Execute(AgsClient client, GeometryServiceResource parent)
        {
            string resourcePath = String.Format("{0}/{1}", parent.resourcePath, resource);
            return (ReshapeResource<TG>)Execute(client, resourcePath);
        }

        public async Task<ReshapeResource<TG>> ExecuteAsync(AgsClient client, GeometryServiceResource parent)
        {
            string resourcePath = String.Format("{0}/{1}", parent.resourcePath, resource);
            var request = createRequest(resourcePath);

            return await client.ExecuteAsync<ReshapeResource<TG>>(request, Method.POST);
        }

        public override BaseResponse Execute(AgsClient client, string resourcePath)
        {
            var request = createRequest(resourcePath);
            var result = client.Execute<ReshapeResource<TG>>(request, Method.POST);

            return result;
        }

        private RestRequest createRequest(string resourcePath)
        {
            var request = new RestRequest(resourcePath) { Method = Method.POST };

            var jss = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };

            if (target != null)
                request.AddParameter("target", JsonConvert.SerializeObject(target, jss));
            if (reshaper != null)
                request.AddParameter("reshaper", JsonConvert.SerializeObject(reshaper, jss));
            if (sr != null)
                request.AddParameter("sr", JsonConvert.SerializeObject(sr, jss));

            return request;
        }
    }
}
