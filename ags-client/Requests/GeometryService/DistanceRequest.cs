using System;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using ags_client.Resources.GeometryService;
using ags_client.Types.Geometry;

namespace ags_client.Requests.GeometryService
{
    public class DistanceRequest<TG1, TG2> : BaseRequest
        where TG1 : IRestGeometry
        where TG2 : IRestGeometry
    {
        public Geometry<TG1> geometry1 { get; set; }
        public Geometry<TG2> geometry2 { get; set; }
        public SpatialReference sr { get; set; }
        public int? distanceUnit { get; set; }
        public bool? geodesic { get; set; }

        const string resource = "distance";

        public DistanceResource Execute(AgsClient client, GeometryServiceResource parent)
        {
            string resourcePath = String.Format("{0}/{1}", parent.resourcePath, resource);
            return (DistanceResource)Execute(client, resourcePath);
        }

        public async Task<DistanceResource> ExecuteAsync(AgsClient client, GeometryServiceResource parent)
        {
            string resourcePath = String.Format("{0}/{1}", parent.resourcePath, resource);
            var request = createRequest(resourcePath);

            return await client.ExecuteAsync<DistanceResource>(request, Method.POST);
        }

        public override BaseResponse Execute(AgsClient client, string resourcePath)
        {
            var request = createRequest(resourcePath);
            var result = client.Execute<DistanceResource>(request, Method.POST);

            return result;
        }

        private RestRequest createRequest(string resourcePath)
        {
            var request = new RestRequest(resourcePath) { Method = Method.POST };

            var jss = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };

            if (geometry1 != null)
                request.AddParameter("geometry1", JsonConvert.SerializeObject(geometry1, jss));
            if (geometry2 != null)
                request.AddParameter("geometry2", JsonConvert.SerializeObject(geometry2, jss));
            if (sr != null)
                request.AddParameter("sr", JsonConvert.SerializeObject(sr, jss));
            if (distanceUnit.HasValue)
                request.AddParameter("distanceUnit", distanceUnit);
            if (geodesic.HasValue)
                request.AddParameter("geodesic", geodesic.Value ? "true" : "false");

            return request;
        }
    }
}
