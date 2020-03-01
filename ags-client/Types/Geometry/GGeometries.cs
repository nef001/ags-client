using ags_client.JsonConverters;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ags_client.Types.Geometry
{
    [JsonConverter(typeof(GGeometriesConverter))]
    public class GGeometries
    {
        public string geometryType { get; set; }
        public List<IRestGeometry> geometries { get; set; }
    }
}
