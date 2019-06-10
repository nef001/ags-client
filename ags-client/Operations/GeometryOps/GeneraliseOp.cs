using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RestSharp;
using Newtonsoft.Json;

using ags_client.Types.Geometry;

namespace ags_client.Operations.GeometryOps
{
    public class GeneralizeOp<TG>
        where TG : IRestGeometry
    {
        public Geometries<TG> geometries { get; set; } //polyline or polygon
        public SpatialReference sr { get; set; }
        public double? maxDeviation { get; set; }
        public int? deviationUnit { get; set; }

        public GeneralizeResponse<TG> Execute(AgsClient client, string servicePath)
        {
            //servicePath is typically "Utilities/Geometry"

            var request = new RestRequest(String.Format("{0}/{1}/{2}", servicePath, "GeometryServer", "generalize"));
            request.Method = Method.POST;

            var jss = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };

            if (geometries != null)
                request.AddParameter("geometries", JsonConvert.SerializeObject(geometries, jss));
            if (sr != null)
                request.AddParameter("sr", JsonConvert.SerializeObject(sr, jss));
            if (maxDeviation.HasValue)
                request.AddParameter("maxDeviation", maxDeviation);
            if (deviationUnit.HasValue)
                request.AddParameter("deviationUnit", deviationUnit);

            var result = client.Execute<GeneralizeResponse<TG>>(request, Method.POST);

            return result;
        }
    }

    public class GeneralizeResponse<TG> : BaseResponse
        where TG : IRestGeometry
    {
        public string geometryType { get; set; }
        public List<TG> geometries { get; set; }
    }
}
