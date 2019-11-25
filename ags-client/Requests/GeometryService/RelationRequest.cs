using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using RestSharp;
using Newtonsoft.Json;
using ags_client.Resources.GeometryService;
using ags_client.Types.Geometry;


namespace ags_client.Requests.GeometryService
{
    public class RelationRequest<TG1, TG2> : BaseRequest
        where TG1 : IRestGeometry
        where TG2 : IRestGeometry
    {
        public Geometries<TG1> geometries1 { get; set; }
        public Geometries<TG2> geometries2 { get; set; }
        public SpatialReference sr { get; set; }
        public string relation { get; set; }
        public string relationParam { get; set; }

        const string resource = "relation";

        public RelationResource Execute(AgsClient client, GeometryServiceResource parent)
        {
            string resourcePath = String.Format("{0}/{1}", parent.resourcePath, resource);
            return (RelationResource)Execute(client, resourcePath);
        }

        public async Task<RelationResource> ExecuteAsync(AgsClient client, GeometryServiceResource parent)
        {
            string resourcePath = String.Format("{0}/{1}", parent.resourcePath, resource);
            var request = createRequest(resourcePath);

            return await client.ExecuteAsync<RelationResource>(request, Method.POST);
        }

        public override BaseResponse Execute(AgsClient client, string resourcePath)
        {
            var request = createRequest(resourcePath);
            return client.Execute<RelationResource>(request, Method.POST);
        }

        private RestRequest createRequest(string resourcePath)
        {
            var request = new RestRequest(resourcePath) { Method = Method.POST };

            var jss = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };

            if (geometries1 != null)
                request.AddParameter("geometries1", JsonConvert.SerializeObject(geometries1, jss));
            if (geometries2 != null)
                request.AddParameter("geometries2", JsonConvert.SerializeObject(geometries2, jss));
            if (sr != null)
                request.AddParameter("sr", JsonConvert.SerializeObject(sr, jss));
            if (!String.IsNullOrWhiteSpace(relation))
                request.AddParameter("relation", relation);
            if (!String.IsNullOrWhiteSpace(relationParam))
                request.AddParameter("relationParam", relationParam);

            request.AddParameter("f", "json");

            return request;
        }
    }
}
