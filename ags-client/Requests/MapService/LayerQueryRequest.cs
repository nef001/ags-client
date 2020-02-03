using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;

using ags_client.Types;
using ags_client.Types.Geometry;
using ags_client.Resources.Common;
using ags_client.Resources.MapService;
using ags_client.JsonConverters;

namespace ags_client.Requests.MapService
{
    public class LayerQueryRequest<TF, TG, TA> : BaseRequest
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
        public DateTime? startTime { get; set; }
        public DateTime? endTime { get; set; }
        public string outFields { get; set; }
        public bool? returnGeometry { get; set; }
        public double? maxAllowableOffset { get; set; }
        public int? geometryPrecision { get; set; }
        public SpatialReference outSR { get; set; }
        public bool? returnIdsOnly { get; set; }
        public string orderByFields { get; set; } //can include ASC or DESC. Example: orderByFields=STATE_NAME ASC, RACE DESC, GENDER
        public List<Statistic> outStatistics { get; set; }
        public string groupByFieldsForStatistics { get; set; }
        public bool? returnZ { get; set; }
        public bool? returnM { get; set; }
        public string gdbVersion { get; set; }
        public bool? returnDistinctValues { get; set; }

        const string resource = "query";

        public LayerQueryResource<TF, TG, TA> Execute(AgsClient client, LayerOrTableResource parent)
        {
            string resourcePath = $"{parent.resourcePath}/{resource}";
            return (LayerQueryResource<TF, TG, TA>)Execute(client, resourcePath);

        }

        public async Task<LayerQueryResource<TF, TG, TA>> ExecuteAsync(AgsClient client, LayerOrTableResource parent)
        {
            string resourcePath = $"{parent.resourcePath}/{resource}";
            var request = createRequest(resourcePath);

            return await client.ExecuteAsync<LayerQueryResource<TF, TG, TA>>(request, Method.POST);
        }

        public override BaseResponse Execute(AgsClient client, string resourcePath)
        {
            var request = createRequest(resourcePath);
            return client.Execute<LayerQueryResource<TF, TG, TA>>(request, Method.POST);
        }

        private RestRequest createRequest(string resourcePath)
        {
            var request = new RestRequest(resourcePath) { Method = Method.POST };

            var jss = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };

            if (!String.IsNullOrEmpty(text))
                request.AddParameter("text", text);
            if (geometry != null)
                request.AddParameter("geometry", JsonConvert.SerializeObject(geometry, jss));
            if (!String.IsNullOrEmpty(geometryType))
                request.AddParameter("geometryType", geometryType);
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

            if (timeInstant.HasValue)
                request.AddParameter("time", JsonConvert.SerializeObject(timeInstant, new DateTimeUnixConverter()));
            else
            {
                if (startTime.HasValue && endTime.HasValue)
                {
                    var converter = new DateTimeUnixConverter();
                    string timeExtent = String.Format("{0}, {1}", JsonConvert.SerializeObject(startTime, converter), JsonConvert.SerializeObject(endTime, converter));
                    request.AddParameter("time", timeExtent);
                }
            }

            if (!String.IsNullOrEmpty(outFields))
                request.AddParameter("outFields", outFields);
            if (returnGeometry.HasValue)
                request.AddParameter("returnGeometry", returnGeometry.Value ? "true" : "false");
            if (maxAllowableOffset.HasValue)
                request.AddParameter("maxAllowableOffset", maxAllowableOffset);
            if (geometryPrecision.HasValue)
                request.AddParameter("geometryPrecision", geometryPrecision);
            if (outSR != null)
                request.AddParameter("outSR", JsonConvert.SerializeObject(outSR, jss));
            if (returnIdsOnly.HasValue)
                request.AddParameter("returnIdsOnly", returnIdsOnly.Value ? "true" : "false");
            if (!String.IsNullOrWhiteSpace(orderByFields))
                request.AddParameter("orderByFields", orderByFields);
            if (outStatistics != null)
                request.AddParameter("outStatistics", JsonConvert.SerializeObject(outStatistics, jss));
            if (!String.IsNullOrWhiteSpace(groupByFieldsForStatistics))
                request.AddParameter("groupByFieldsForStatistics", groupByFieldsForStatistics);
            if (returnZ.HasValue)
                request.AddParameter("returnZ", returnZ.Value ? "true" : "false");
            if (returnM.HasValue)
                request.AddParameter("returnM", returnM.Value ? "true" : "false");
            if (!String.IsNullOrEmpty(gdbVersion))
                request.AddParameter("gdbVersion", gdbVersion);
            if (returnDistinctValues.HasValue)
                request.AddParameter("returnDistinctValues", returnDistinctValues.Value ? "true" : "false");

            request.AddParameter("f", "json");

            return request;
        }
    }
}
