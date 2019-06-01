using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ags_client.Types.Geometry
{
    public class MultiPoint : PointArray, IRestGeometry
    {
        public SpatialReference spatialReference { get; set; }

        [JsonProperty("points")]
        private double[][] _pointsArray;

        [OnSerializing]
        internal void OnSerializing(StreamingContext context)
        {
            _pointsArray = base.ToArray();
        }

        [OnDeserialized]
        internal void OnDeserialized(StreamingContext context)
        {
            Points = new List<Coordinate>();
            foreach (var currentPoint in _pointsArray.Select(point => new Coordinate { x = point[0], y = point[1] }))
            {
                Points.Add(currentPoint);
            }

            if (Points.Any(x => x == null)) { throw new InvalidOperationException("The collection may not contain a null point."); }
        }
    }
}
