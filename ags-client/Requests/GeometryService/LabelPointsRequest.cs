using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using ags_client.Resources.GeometryService;
using ags_client.Types.Geometry;

namespace ags_client.Requests.GeometryService
{
    public class LabelPointsRequest : BaseRequest
    {
        public List<Polygon> polygons { get; set; }
        public SpatialReference sr { get; set; }

        const string resource = "labelPoints";

        public LabelPointsResource Execute(AgsClient client, GeometryServiceResource parent)
        {
            string resourcePath = String.Format("{0}/{1}", parent.resourcePath, resource);
            return (LabelPointsResource)Execute(client, resourcePath);
        }

        public async Task<LabelPointsResource> ExecuteAsync(AgsClient client, GeometryServiceResource parent)
        {
            string resourcePath = String.Format("{0}/{1}", parent.resourcePath, resource);
            var request = createRequest(resourcePath);

            return await client.ExecuteAsync<LabelPointsResource>(request, Method.POST);
        }

        public override BaseResponse Execute(AgsClient client, string resourcePath)
        {
            var request = createRequest(resourcePath);
            var result = client.Execute<LabelPointsResource>(request, Method.POST);

            return result;
        }

        private RestRequest createRequest(string resourcePath)
        {
            var request = new RestRequest(resourcePath) { Method = Method.POST };

            var jss = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };

            if (polygons != null)
                request.AddParameter("polygons", JsonConvert.SerializeObject(polygons, jss));
            if (sr != null)
                request.AddParameter("sr", JsonConvert.SerializeObject(sr, jss));

            return request;
        }
    }
}
