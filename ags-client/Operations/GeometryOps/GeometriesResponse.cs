using System.Collections.Generic;

using ags_client.Types.Geometry;

namespace ags_client.Operations.GeometryOps
{
    public class GeometriesResponse : BaseResponse
    {
        public List<IRestGeometry> geometries { get; set; }
    }
}
