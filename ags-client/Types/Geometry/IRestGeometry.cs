using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ags_client.Types.Geometry
{
    public interface IRestGeometry
    {
        string geometryType { get; set; }
        SpatialReference spatialReference { get; set; }
    }
}
