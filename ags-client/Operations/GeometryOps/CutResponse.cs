using System.Collections.Generic;

using ags_client.Types.Geometry;
using ags_client.JsonConverters;

using Newtonsoft.Json;

namespace ags_client.Operations.GeometryOps
{
    [JsonConverter(typeof(CutResponseConverter))]
    public class CutResponse : BaseResponse
    {
        public string geometryType { get; set; }
        public List<IRestGeometry> geometries { get; set; }
        public List<int> cutIndexes { get; set; }
    }

    
}
