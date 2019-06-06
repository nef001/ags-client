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
    public class AutoCompleteOp
    {
        public List<Polygon> polygons { get; set; }
        public List<Polyline> polylines { get; set; }
        public SpatialReference sr { get; set; }

        public AutoCompleteResponse Execute(AgsClient client, string servicePath)
        {
            //servicePath is typically "Utilities/Geometry"

            var request = new RestRequest(String.Format("{0}/{1}/{2}", servicePath, "GeometryServer", "autoComplete"));
            request.Method = Method.POST;

            var jss = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };

            if (polygons != null)
                request.AddParameter("polygons", JsonConvert.SerializeObject(polygons, jss));
            if (polylines != null)
                request.AddParameter("polylines", JsonConvert.SerializeObject(polylines, jss));
            if (sr != null)
                request.AddParameter("sr", sr.wkid);
            

            var result = client.Execute<AutoCompleteResponse>(request, Method.POST);

            return result;
        }
    }
}
