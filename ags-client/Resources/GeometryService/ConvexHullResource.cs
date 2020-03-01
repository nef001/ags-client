
using ags_client.JsonConverters;
using ags_client.Types.Geometry;
using Newtonsoft.Json;

namespace ags_client.Resources.GeometryService
{
    /* The convex hull is typically a polygon but can also be a polyline or point in degenerate cases.
     * So a custom converter is used to determine the geometry object to instantiate when deserializing. */
    [JsonConverter(typeof(ConvexHullResourceConverter))]
    public class ConvexHullResource : BaseResponse
    {
        public string geometryType { get; set; }
        public IRestGeometry geometry { get; set; }
    }
}
