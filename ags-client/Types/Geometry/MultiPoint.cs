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
            /*
            < multipoint tagged text> ::= multipoint < multipoint text >
            */

            return $"MULTIPOINT {multipointText()}";
        }

        private string multipointText()
        {
            /*
            < multipoint text > ::= < empty set > | 
            < left paren > < point text > {< comma > < point text >}* < right paren >

            i.e. same as a linestring

            */

            return this.LineStringText(false);
        }

        
    }
}
