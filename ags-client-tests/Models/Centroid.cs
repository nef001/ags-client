using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ags_client.Types;
using ags_client.Types.Geometry;


namespace ags_client_tests.Models
{
    public class Centroid : IRestFeature<Point, CentroidA>
    {
        public Point geometry { get; set; }
        public CentroidA attributes { get; set; }
    }

    public class CentroidA : IRestAttributes
    {
        public int? objectid { get; set; }
        public int pin { get; set; }
        public string tag { get; set; }

    }
}
