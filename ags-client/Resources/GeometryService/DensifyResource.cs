using System.Collections.Generic;

using ags_client.Types.Geometry;

namespace ags_client.Resources.GeometryService
{
    public class DensifyResource<TG> : BaseResponse
        where TG : IRestGeometry
    {
        public string geometryType { get; set; }
        public List<TG> geometries { get; set; }
    }
}
