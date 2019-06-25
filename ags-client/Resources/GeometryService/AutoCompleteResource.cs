using System.Collections.Generic;

using ags_client.Types.Geometry;

namespace ags_client.Resources.GeometryService
{
    public class AutoCompleteResource : BaseResponse
    {
        public List<Polygon> geometries { get; set; }
        public SpatialReference spatialReference { get; set; }
    }
}
