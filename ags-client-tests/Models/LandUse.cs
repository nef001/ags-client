
using ags_client.Types;
using ags_client.Types.Geometry;

namespace ags_client_tests.Models
{
    public class LandUse : IRestFeature<Polygon, LandUseA>
    {
        public Polygon geometry { get; set; }
        public LandUseA attributes { get; set; }
    }

    public class LandUseA : IRestAttributes
    {
        public int? objectid { get; set; }
        public int landuse_code { get; set; }
        public string landuse_name { get; set; }

    }
}
