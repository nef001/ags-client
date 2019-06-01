using System;
using System.Collections.Generic;

using RestSharp;
using Newtonsoft.Json;

using ags_client.Types;
using ags_client.Types.Geometry;

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

        public LayerQueryResponse<TF, TG, TA> Execute(
            AgsClient client, string servicePath, string serviceType, int layerId)
        {
            var request = new RestRequest(String.Format("{0}/{1}/{2}/query", servicePath, serviceType, layerId));

            if (!String.IsNullOrEmpty(text))
                request.AddParameter("text", text);
            if (geometry != null)
                request.AddParameter("geometry", JsonConvert.SerializeObject(geometry));
            if (inSR != null)
                request.AddParameter("inSR", JsonConvert.SerializeObject(inSR));
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
                request.AddParameter("returnGeometry", returnGeometry);           
            if (outSR != null)
                request.AddParameter("outSR", JsonConvert.SerializeObject(outSR));
            if (returnIdsOnly.HasValue)
                request.AddParameter("returnIdsOnly", returnIdsOnly);
            if (returnCountOnly.HasValue)
                request.AddParameter("returnCountOnly", returnCountOnly);
            if ((orderByFields != null) && (orderByFields.Count > 0))
                request.AddParameter("orderByFields", String.Join(", ", orderByFields));
            if (returnZ.HasValue)
                request.AddParameter("returnZ", returnZ);
            if (returnM.HasValue)
                request.AddParameter("returnM", returnM);
            if (!String.IsNullOrEmpty(gdbVersion))
                request.AddParameter("gdbVersion", gdbVersion);
            if (returnDistinctValues.HasValue)
                request.AddParameter("returnDistinctValues", returnDistinctValues);

            var result = client.Execute<LayerQueryResponse<TF, TG, TA>>(request, Method.POST);

            return result;
        }


        
    }
}
