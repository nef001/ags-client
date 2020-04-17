using ags_client.Types.Geometry;
using System.Collections.Generic;

namespace ags_client.Resources.GeometryService
{
    public class TrimExtendResource : BaseResponse
    {
        public string geometryType { get; set; }
        public List<Polyline> geometries { get; set; }
    }
}
