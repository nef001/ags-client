using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ags_client.Types.Geometry
{
    public class Geometry<TG>
        where TG : IRestGeometry
    {
        public string geometryType { get; set; }
        public TG geometry { get; set; }
    }
}
