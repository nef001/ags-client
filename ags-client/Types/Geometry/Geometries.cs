using System.Collections.Generic;

namespace ags_client.Types.Geometry
{
    public class Geometries<TG>
        where TG : IRestGeometry
    {
        public string geometryType { get; set; }
        public List<TG> geometries { get; set; }
    }
}
