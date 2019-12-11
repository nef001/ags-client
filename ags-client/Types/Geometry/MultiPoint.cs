using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ags_client.Types.Geometry
{
    public class MultiPoint : Path, IRestGeometry
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
            Coordinates = new List<Coordinate>();
            foreach (var currentCoord in _pointsArray.Select(coord => new Coordinate { x = coord[0], y = coord[1] }))
            {
                Coordinates.Add(currentCoord);
            }

            if (Coordinates.Any(x => x == null)) { throw new InvalidOperationException("The collection may not contain a null point."); }
        }

        public string ToWkt()
        {
            if (Coordinates == null)
                return "MULTIPOINT EMPTY";

            Coordinates.RemoveAll(x => x == null);

            if (Coordinates.Count == 0) 
                return "MULTIPOINT EMPTY";

            var sb = new StringBuilder("MULTIPOINT(");
            sb.Append(CoordinatesText());
            sb.Append(")");

            return sb.ToString();
        }

        public override string ToString()
        {
            return ToWkt();
        }
    }
}
