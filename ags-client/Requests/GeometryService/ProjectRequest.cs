using ags_client.Resources.GeometryService;
using ags_client.Types.Geometry;
using Newtonsoft.Json;
using RestSharp;
using System.Threading.Tasks;

namespace ags_client.Requests.GeometryService
{
    public class ProjectRequest<TG> : BaseRequest
        where TG : IRestGeometry
    {
        public Geometries<TG> geometries { get; set; }
        public SpatialReference inSR { get; set; }
        public SpatialReference outSR { get; set; }
        public Transformation transformation { get; set; }
        public bool? transformForward { get; set; }

        const string resource = "project";

        public ProjectResource<TG> Execute(AgsClient client, GeometryServiceResource parent)
        {
            string resourcePath = $"{parent.resourcePath}/{resource}";
            return (ProjectResource<TG>)Execute(client, resourcePath);
        }

        public async Task<ProjectResource<TG>> ExecuteAsync(AgsClient client, GeometryServiceResource parent)
        {
            string resourcePath = $"{parent.resourcePath}/{resource}";
            var request = createRequest(resourcePath);

            return await client.ExecuteAsync<ProjectResource<TG>>(request, Method.POST);
        }

        public override BaseResponse Execute(AgsClient client, string resourcePath)
        {
            var request = createRequest(resourcePath);
            return client.Execute<ProjectResource<TG>>(request, Method.POST);
        }

        private RestRequest createRequest(string resourcePath)
        {
            var request = new RestRequest(resourcePath) { Method = Method.POST };

            var jss = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };

            if (geometries != null)
                request.AddParameter("geometries", JsonConvert.SerializeObject(geometries, jss));
            if (inSR != null)
                request.AddParameter("inSR", inSR.wkid);
            if (outSR != null)
                request.AddParameter("outSR", outSR.wkid);
            if (transformation != null)
                request.AddParameter("transformation", JsonConvert.SerializeObject(transformation, jss));
            if (transformForward.HasValue)
                request.AddParameter("transformForward", transformForward.Value ? "true" : "false");

            request.AddParameter("f", "json");

            return request;
        }




    }
}
