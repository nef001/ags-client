using System.Collections.Generic;

using ags_client.Types.Geometry;
using ags_client.JsonConverters;

using Newtonsoft.Json;

namespace ags_client.Operations.GeometryOps
{
    [JsonConverter(typeof(DensifyResponseConverter))]
    public class DensifyResponse : BaseResponse
    {
        public string geometryType { get; set; }
        public List<IRestGeometry> geometries { get; set; }
    }
}
