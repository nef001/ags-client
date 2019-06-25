using System;
using RestSharp;
using Newtonsoft.Json;
using ags_client.Resources.GeometryService;
using ags_client.Types.Geometry;

namespace ags_client.Requests.GeometryService
{
    public class DifferenceRequest<TG1, TG2> : BaseRequest
        where TG1 : IRestGeometry
        where TG2 : IRestGeometry
    {
        public Geometries<TG1> geometries { get; set; }
        public Geometry<TG2> geometry { get; set; }
        public SpatialReference sr { get; set; } //wkid of input geometries

        public DifferenceResource<TG1> Execute(AgsClient client, GeometryServiceResource parent)
        {
            string resourcePath = String.Format("{0}/difference", parent.resourcePath);
            return (DifferenceResource<TG1>)Execute(client, resourcePath);
        }
        public override BaseResponse Execute(AgsClient client, string resourcePath)
        {
            var request = new RestRequest(resourcePath) { Method = Method.POST };

            var jss = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };

            if (geometries != null)
                request.AddParameter("geometries", JsonConvert.SerializeObject(geometries, jss));
            if (geometry != null)
                request.AddParameter("geometry", JsonConvert.SerializeObject(geometry, jss));
            if (sr != null)
                request.AddParameter("sr", JsonConvert.SerializeObject(sr, jss));

            var result = client.Execute<DifferenceResource<TG1>>(request, Method.POST);

            return result;
        }
    }
}
