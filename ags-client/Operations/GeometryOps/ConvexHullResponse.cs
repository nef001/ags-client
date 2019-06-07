
using ags_client.Types.Geometry;
using ags_client.JsonConverters;

using Newtonsoft.Json;

namespace ags_client.Operations.GeometryOps
{
    /* The convex hull is typically a polygon but can also be a polyline or point in degenerate cases.
     * So we use a custom converter to determine the geometry object to instantiate when deserializing. */
    [JsonConverter(typeof(ConvexHullResponseConverter))]
    public class ConvexHullResponse : BaseResponse
    {
        public string geometryType { get; set; }
        public IRestGeometry geometry { get; set; }

    }

    
}
