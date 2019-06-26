using System;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using ags_client.Resources.GeometryService;
using ags_client.Types.Geometry;

namespace ags_client.Requests.GeometryService
{
    public class GeneralizeRequest<TG> : BaseRequest
        where TG : IRestGeometry
    {
        public Geometries<TG> geometries { get; set; } //polyline or polygon
        public SpatialReference sr { get; set; }
        public double? maxDeviation { get; set; }
        public int? deviationUnit { get; set; }

        public GeneralizeResource<TG> Execute(AgsClient client, GeometryServiceResource parent)
        {
            string resourcePath = String.Format("{0}/generalize", parent.resourcePath);
            return (GeneralizeResource<TG>)Execute(client, resourcePath);
        }
        public override BaseResponse Execute(AgsClient client, string resourcePath)
        {
            var request = createRequest(resourcePath);
            var result = client.Execute<GeneralizeResource<TG>>(request, Method.POST);

            return result;
        }

        private RestRequest createRequest(string resourcePath)
        {
            var request = new RestRequest(resourcePath) { Method = Method.POST };

            var jss = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };

            if (geometries != null)
                request.AddParameter("geometries", JsonConvert.SerializeObject(geometries, jss));
            if (sr != null)
                request.AddParameter("sr", JsonConvert.SerializeObject(sr, jss));
            if (maxDeviation.HasValue)
                request.AddParameter("maxDeviation", maxDeviation);
            if (deviationUnit.HasValue)
                request.AddParameter("deviationUnit", deviationUnit);

            return request;
        }
    }
}
