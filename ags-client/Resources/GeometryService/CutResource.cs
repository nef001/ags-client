using System.Collections.Generic;

using Newtonsoft.Json;

using ags_client.Types.Geometry;
using ags_client.JsonConverters;

namespace ags_client.Resources.GeometryService
{
    [JsonConverter(typeof(CutResourceConverter))]
    public class CutResource : BaseResponse
    {
        public string geometryType { get; set; }
        public List<IRestGeometry> geometries { get; set; }
        public List<int> cutIndexes { get; set; }
    }
}
