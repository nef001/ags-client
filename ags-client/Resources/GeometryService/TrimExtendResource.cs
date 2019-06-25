using System.Collections.Generic;

using ags_client.Types.Geometry;

namespace ags_client.Resources.GeometryService
{
    public class TrimExtendResource : BaseResponse
    {
        public string geometryType { get; set; }
        public List<Polyline> geometries { get; set; }
    }
}
