using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using RestSharp;
using Newtonsoft.Json;
using ags_client.Resources.GeometryService;
using ags_client.Types.Geometry;

namespace ags_client.Requests.GeometryService
{
    public class BufferRequest<TG> : BaseRequest
        where TG : IRestGeometry
    {
        public Geometries<TG> geometries { get; set; }
        public SpatialReference inSR { get; set; }
        public SpatialReference outSR { get; set; }
        public SpatialReference bufferSR { get; set; }
        public List<double> distances { get; set; }
        public int? unit { get; set; } /* See esriSRUnitType Constants and esriSRUnit2Type Constants */
        public bool? unionResults { get; set; }
        public bool? geodesic { get; set; }

        public BufferResource<Polygon> Execute(AgsClient client, GeometryServiceResource parent)
        {
            string resourcePath = String.Format("{0}/buffer", parent.resourcePath);
            return (BufferResource<Polygon>)Execute(client, resourcePath);
        }
        public override BaseResponse Execute(AgsClient client, string resourcePath)
        {
            var request = createRequest(resourcePath);
            var result = client.Execute<BufferResource<Polygon>>(request, Method.POST);

            return result;
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
            if (bufferSR != null)
                request.AddParameter("bufferSR", bufferSR.wkid);
            if ((distances != null) && (distances.Count > 0))
                request.AddParameter("distances", String.Join(",", distances.Select(n => n.ToString()).ToArray()));
            if (unit.HasValue)
                request.AddParameter("unit", unit);
            if (unionResults.HasValue)
                request.AddParameter("unionResults", unionResults.Value ? "true" : "false");
            if (geodesic.HasValue)
                request.AddParameter("geodesic", geodesic.Value ? "true" : "false");

            return request;
        }
    }
}
