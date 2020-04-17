using ags_client.Types.Geometry;
using System.Collections.Generic;

namespace ags_client.Resources.GeometryService
{
    public class AutoCompleteResource : BaseResponse
    {
        public List<Polygon> geometries { get; set; }
        public SpatialReference spatialReference { get; set; }
    }
}
