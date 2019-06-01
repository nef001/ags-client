using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace ags_client.Types.Geometry
{
    public class NoGeometry : IRestGeometry
    {
        [JsonProperty("spatialReference")]
        public SpatialReference spatialReference { get; set; }
    }
}
