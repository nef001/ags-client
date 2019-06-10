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
    /* foreach TG1 in geometries constructs TG1 - TG2 geometry */
    public class DifferenceOp<TG1, TG2>
        where TG1 : IRestGeometry
        where TG2 : IRestGeometry
    {
        public Geometries<TG1> geometries { get; set; }
        public Geometry<TG2> geometry { get; set; }
        public SpatialReference sr { get; set; } //wkid of input geometries

        public DifferenceResponse<TG1> Execute(AgsClient client, string servicePath)
        {
            //servicePath is typically "Utilities/Geometry"

            var request = new RestRequest(String.Format("{0}/{1}/{2}", servicePath, "GeometryServer", "difference"));
            request.Method = Method.POST;

            var jss = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };

            if (geometries != null)
                request.AddParameter("geometries", JsonConvert.SerializeObject(geometries, jss));
            if (geometry != null)
                request.AddParameter("geometry", JsonConvert.SerializeObject(geometry, jss));
            if (sr != null)
                request.AddParameter("sr", JsonConvert.SerializeObject(sr, jss));
            

            var result = client.Execute<DifferenceResponse<TG1>>(request, Method.POST);

            return result;
        }
    }

    public class DifferenceResponse<TG> : BaseResponse
        where TG : IRestGeometry
    {
        public string geometryType { get; set; }
        public List<TG> geometries { get; set; }
    }
}
