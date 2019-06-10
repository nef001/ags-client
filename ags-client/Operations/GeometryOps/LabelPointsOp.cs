using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ags_client.Types.Geometry;

using RestSharp;
using Newtonsoft.Json;

namespace ags_client.Operations.GeometryOps
{
    public class LabelPointsOp
    {
        public List<Polygon> polygons { get; set; }
        public SpatialReference sr { get; set; }

        public LabelPointsOpResponse Execute(AgsClient client, string servicePath)
        {
            var request = new RestRequest(String.Format("{0}/{1}/{2}", servicePath, "GeometryServer", "labelPoints"));
            request.Method = Method.POST;

            var jss = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };

            if (polygons != null)
                request.AddParameter("polygons", JsonConvert.SerializeObject(polygons, jss));
            
            if (sr != null)
                request.AddParameter("sr", JsonConvert.SerializeObject(sr, jss));

            var result = client.Execute<LabelPointsOpResponse>(request, Method.POST);

            return result;
        }


    }

    public class LabelPointsOpResponse : BaseResponse
    {
        public List<Point> labelPoints { get; set; }
    }
}
