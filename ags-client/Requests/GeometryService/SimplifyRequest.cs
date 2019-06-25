using System;
using RestSharp;
using Newtonsoft.Json;
using ags_client.Resources.GeometryService;
using ags_client.Types.Geometry;

namespace ags_client.Requests.GeometryService
{
    public class SimplifyRequest<TG> : BaseRequest
        where TG : IRestGeometry
    {
        public Geometries<TG> geometries { get; set; }
        public SpatialReference sr { get; set; }

        public SimplifyResource<TG> Execute(AgsClient client, GeometryServiceResource parent)
        {
            string resourcePath = String.Format("{0}/reshape", parent.resourcePath);
            return (SimplifyResource<TG>)Execute(client, resourcePath);
        }
        public override BaseResponse Execute(AgsClient client, string resourcePath)
        {
            var request = new RestRequest(resourcePath) { Method = Method.POST };

            var jss = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };

            if (geometries != null)
                request.AddParameter("geometries", JsonConvert.SerializeObject(geometries, jss));
            if (sr != null)
                request.AddParameter("sr", JsonConvert.SerializeObject(sr, jss));

            var result = client.Execute<SimplifyResource<TG>>(request, Method.POST);

            return result;
        }
    }
}
