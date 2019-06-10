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
    public class AreasAndLengthsOp
    {
        public List<Polygon> polygons { get; set; }
        public SpatialReference sr { get; set; }
        public string lengthUnit { get; set; }
        public string areaUnit { get; set; }
        public string calculationType { get; set; }

        public AreasAndLengthsResponse Execute(AgsClient client, string servicePath)
        {
            //servicePath is typically "Utilities/Geometry"

            var request = new RestRequest(String.Format("{0}/{1}/{2}", servicePath, "GeometryServer", "areasAndLengths"));
            request.Method = Method.POST;

            var jss = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };

            if (polygons != null)
                request.AddParameter("polygons", JsonConvert.SerializeObject(polygons, jss));
            if (sr != null)
                request.AddParameter("sr", sr.wkid);
            if (!String.IsNullOrWhiteSpace(lengthUnit))
                request.AddParameter("lengthUnit", lengthUnit);
            if (!String.IsNullOrWhiteSpace(areaUnit))
                request.AddParameter("areaUnit", areaUnit);
            if (!String.IsNullOrWhiteSpace(calculationType))
                request.AddParameter("calculationType", calculationType);

            var result = client.Execute<AreasAndLengthsResponse>(request, Method.POST);

            return result;
        }

    }

    public class AreasAndLengthsResponse : BaseResponse
    {
        public List<double> areas { get; set; }
        public List<double> lengths { get; set; }
    }
}
