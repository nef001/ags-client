using System;
using System.Text;
using System.Collections.Generic;
using System.Net;

using RestSharp;
using Newtonsoft.Json;

using ags_client.Types;
using ags_client.Types.Geometry;
using ags_client.Utility;

namespace ags_client.Operations
{
    public class LayerQueryOperation<TF, TG, TA>
        where TF : IRestFeature<TG, TA>
        where TG : IRestGeometry
        where TA : IRestAttributes
    {
        public string text { get; set; }
        public IRestGeometry geometry { get; set; }
        public string geometryType { get; set; }
        public SpatialReference inSR { get; set; }
        public string spatialRel { get; set; }
        public string relationParam { get; set; }
        public string where { get; set; }
        public List<int> objectIds { get; set; }
        public DateTime? timeInstant { get; set; }
        public TimeSpan? timeExtent { get; set; }
        public List<string> outFields { get; set; }
        public bool? returnGeometry { get; set; }
        public double? maxAllowableOffset { get; set; }
        public int? geometryPrecision { get; set; }
        public SpatialReference outSR { get; set; }
        public bool? returnIdsOnly { get; set; }
        public bool? returnCountOnly { get; set; }
        public List<string> orderByFields { get; set; } //can include ASC or DESC. Example: orderByFields=STATE_NAME ASC, RACE DESC, GENDER
        public bool? returnZ { get; set; }
        public bool? returnM { get; set; }
        public string gdbVersion { get; set; }
        public bool? returnDistinctValues { get; set; }

        public LayerQueryOperation()
        {
            outFields = new List<string>();
            orderByFields = new List<string>();
        }

        public LayerQueryResponse<TF, TG, TA> Execute2(
            AgsClient client, string servicePath, string serviceType, int layerId)
        {
            string url = String.Format("{0}/{1}/{2}/{3}/{4}", client.BaseUrl, servicePath, serviceType, layerId, "query");

            var jss = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };

            StringBuilder parameters = new StringBuilder("f=pjson");
            if (!String.IsNullOrEmpty(text))
                parameters.AppendFormat("&text={0}", WebUtility.UrlEncode(text));
            if (geometry != null)
                parameters.AppendFormat("&geometry={0}", JsonConvert.SerializeObject(geometry, jss));
            if (inSR != null)
                parameters.AppendFormat("&inSR={0}", JsonConvert.SerializeObject(inSR, jss));
            if (!String.IsNullOrEmpty(spatialRel))
                parameters.AppendFormat("&spatialRel={0}", spatialRel);
            if (!String.IsNullOrEmpty(relationParam))
                parameters.AppendFormat("&relationParam={0}", relationParam);
            if (!String.IsNullOrEmpty(where))
                parameters.AppendFormat("&where={0}", WebUtility.UrlEncode(where));
            if ((objectIds != null) && (objectIds.Count > 0))
                parameters.AppendFormat("&objectIds={0}", String.Join(", ", objectIds));
            if ((outFields != null) && (outFields.Count > 0))
                parameters.AppendFormat("&outFields={0}", String.Join(", ", outFields));
            if (returnGeometry.HasValue)
                parameters.AppendFormat("&returnGeometry={0}", returnGeometry.Value ? "true" : "false");
            if (outSR != null)
                parameters.AppendFormat("&outSR={0}", JsonConvert.SerializeObject(outSR, jss));
            if (returnIdsOnly.HasValue)
                parameters.AppendFormat("&returnIdsOnly={0}", returnIdsOnly.Value ? "true" : "false");
            if (returnCountOnly.HasValue)
                parameters.AppendFormat("&returnCountOnly={0}", returnCountOnly.Value ? "true" : "false");
            if ((orderByFields != null) && (orderByFields.Count > 0))
                parameters.AppendFormat("&orderByFields={0}", String.Join(", ", orderByFields));
            if (returnZ.HasValue)
                parameters.AppendFormat("&returnZ={0}", returnZ.Value ? "true" : "false");
            if (returnM.HasValue)
                parameters.AppendFormat("&returnM={0}", returnM.Value ? "true" : "false");
            if (!String.IsNullOrEmpty(gdbVersion))
                parameters.AppendFormat("&gdbVersion={0}", gdbVersion);
            if (returnDistinctValues.HasValue)
                parameters.AppendFormat("&returnDistinctValues={0}", returnDistinctValues.Value ? "true" : "false");

            string requestBody = parameters.ToString();

            string responseString = HttpUtil.GetResponse(url, requestBody);

            var result = JsonConvert.DeserializeObject<LayerQueryResponse<TF, TG, TA>>(responseString, jss);

            return result;
        }

        public LayerQueryResponse<TF, TG, TA> Execute(
            AgsClient client, string servicePath, string serviceType, int layerId)
        {
            var request = new RestRequest(String.Format("{0}/{1}/{2}/query", servicePath, serviceType, layerId));

            var jss = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };

            if (!String.IsNullOrEmpty(text))
                request.AddParameter("text", text);
            if (geometry != null)
                request.AddParameter("geometry", JsonConvert.SerializeObject(geometry, jss));
            if (inSR != null)
                request.AddParameter("inSR", JsonConvert.SerializeObject(inSR, jss));
            if (!String.IsNullOrEmpty(spatialRel))
                request.AddParameter("spatialRel", spatialRel);
            if (!String.IsNullOrEmpty(relationParam))
                request.AddParameter("relationParam", relationParam);
            if (!String.IsNullOrEmpty(where))
                request.AddParameter("where", where);
            if ((objectIds != null) && (objectIds.Count > 0))
                request.AddParameter("objectIds", String.Join(", ", objectIds));            
            if ((outFields != null) && (outFields.Count > 0))
                request.AddParameter("outFields", String.Join(", ", outFields));            
            if (returnGeometry.HasValue)
                request.AddParameter("returnGeometry", returnGeometry.Value ? "true" : "false");
            if (outSR != null)
                request.AddParameter("outSR", JsonConvert.SerializeObject(outSR, jss));
            if (returnIdsOnly.HasValue)
                request.AddParameter("returnIdsOnly", returnIdsOnly.Value ? "true" : "false");
            if (returnCountOnly.HasValue)
                request.AddParameter("returnCountOnly", returnCountOnly.Value ? "true" : "false");
            if ((orderByFields != null) && (orderByFields.Count > 0))
                request.AddParameter("orderByFields", String.Join(", ", orderByFields));
            if (returnZ.HasValue)
                request.AddParameter("returnZ", returnZ.Value ? "true" : "false");
            if (returnM.HasValue)
                request.AddParameter("returnM", returnM.Value ? "true" : "false");
            if (!String.IsNullOrEmpty(gdbVersion))
                request.AddParameter("gdbVersion", gdbVersion);
            if (returnDistinctValues.HasValue)
                request.AddParameter("returnDistinctValues", returnDistinctValues.Value ? "true" : "false");

            var result = client.Execute<LayerQueryResponse<TF, TG, TA>>(request, Method.POST);

            return result;
        }


        
    }
}
