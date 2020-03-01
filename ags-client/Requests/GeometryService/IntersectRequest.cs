using ags_client.Resources.GeometryService;
using ags_client.Types.Geometry;
using Newtonsoft.Json;
using RestSharp;
using System.Threading.Tasks;

namespace ags_client.Requests.GeometryService
{
    public class IntersectRequest<TG1, TG2> : BaseRequest
        where TG1 : IRestGeometry
        where TG2 : IRestGeometry
    {
        public Geometries<TG1> geometries { get; set; }
        public Geometry<TG2> geometry { get; set; }
        public SpatialReference sr { get; set; } //wkid of input geometries

        const string resource = "intersect";

        public IntersectResource<TG1> Execute(AgsClient client, GeometryServiceResource parent)
        {
            string resourcePath = $"{parent.resourcePath}/{resource}";
            return (IntersectResource<TG1>)Execute(client, resourcePath);
        }

        public async Task<IntersectResource<TG1>> ExecuteAsync(AgsClient client, GeometryServiceResource parent)
        {
            string resourcePath = $"{parent.resourcePath}/{resource}";
            var request = createRequest(resourcePath);

            return await client.ExecuteAsync<IntersectResource<TG1>>(request, Method.POST);
        }

        public override BaseResponse Execute(AgsClient client, string resourcePath)
        {
            var request = createRequest(resourcePath);
            return client.Execute<IntersectResource<TG1>>(request, Method.POST);
        }

        private RestRequest createRequest(string resourcePath)
        {
            var request = new RestRequest(resourcePath) { Method = Method.POST };

            var jss = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };

            if (geometries != null)
                request.AddParameter("geometries", JsonConvert.SerializeObject(geometries, jss));
            if (geometry != null)
                request.AddParameter("geometry", JsonConvert.SerializeObject(geometry, jss));
            if (sr != null)
                request.AddParameter("sr", JsonConvert.SerializeObject(sr, jss));

            request.AddParameter("f", "json");

            return request;
        }
    }
}
