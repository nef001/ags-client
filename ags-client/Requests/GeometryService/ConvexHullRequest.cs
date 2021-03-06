﻿using ags_client.Resources.GeometryService;
using ags_client.Types.Geometry;
using Newtonsoft.Json;
using RestSharp;
using System.Threading.Tasks;

namespace ags_client.Requests.GeometryService
{
    public class ConvexHullRequest<TG> : BaseRequest
        where TG : IRestGeometry
    {
        public Geometries<TG> geometries { get; set; }
        public SpatialReference sr { get; set; }

        const string resource = "convexHull";

        public ConvexHullResource Execute(AgsClient client, GeometryServiceResource parent)
        {
            string resourcePath = $"{parent.resourcePath}/{resource}";
            return (ConvexHullResource)Execute(client, resourcePath);
        }

        public async Task<ConvexHullResource> ExecuteAsync(AgsClient client, GeometryServiceResource parent)
        {
            string resourcePath = $"{parent.resourcePath}/{resource}";
            var request = createRequest(resourcePath);

            return await client.ExecuteAsync<ConvexHullResource>(request, Method.POST);
        }

        public override BaseResponse Execute(AgsClient client, string resourcePath)
        {
            var request = createRequest(resourcePath);
            return client.Execute<ConvexHullResource>(request, Method.POST);
        }

        private RestRequest createRequest(string resourcePath)
        {
            var request = new RestRequest(resourcePath) { Method = Method.POST };

            var jss = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };

            if (geometries != null)
                request.AddParameter("geometries", JsonConvert.SerializeObject(geometries, jss));
            if (sr != null)
                request.AddParameter("sr", sr.wkid);

            request.AddParameter("f", "json");

            return request;
        }
    }
}
