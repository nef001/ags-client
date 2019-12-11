using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.Serialization;

using Newtonsoft.Json;

namespace ags_client.Types.Geometry
{

    public class Polygon : IRestGeometry
    {
        public SpatialReference spatialReference { get; set; }

        [JsonProperty("rings")]
        private double[][][] _ringsArray;

        [JsonIgnore]
        public List<Path> Rings { get; set; }

        public double[][][] ToArray()
        {
            double[][][] result = new double[Rings.Count][][];
            for (var i = 0; i < Rings.Count; i++)
            {
                result[i] = Rings.ElementAt(i).ToArray();
            }
            return result;
        }

        [OnSerializing]
        internal void OnSerializing(StreamingContext context)
        {
            _ringsArray = ToArray();
        }

        [OnDeserialized]
        internal void OnDeserialized(StreamingContext context)
        {
            Rings = new List<Path>();
            foreach (var currentRingPointList in _ringsArray.Select(
                ring => ring.Select(
                    point => new Coordinate { x = point[0], y = point[1] }).ToList()))
            {
                Rings.Add(new Path { Coordinates = currentRingPointList });
            }

            if (Rings.Any(x => x == null)) { throw new InvalidOperationException("The collection may not contain a null ring."); }
        }

        public override string ToString()
        {
            try
            {
                return ToWkt();
            }
            catch(Exception e)
            {
                return "Polygon invalid. " + e.Message;
            }
        }

        public string ToWkt()
        {
            if (Rings == null)
                return "POLYGON EMPTY";

            Rings.RemoveAll(x => x == null);

            if (Rings.Count == 0)
                return "POLYGON EMPTY";

            var sb = new StringBuilder();

            if (Rings.Count == 1) //simple case - 1 outer ring, no inner rings.
            {
                sb.Append("POLYGON (");
                sb.Append(String.Join(",", Rings.Select(x => x.CoordinatesText(true)))); //assumption we need to reverse esri orientation
                sb.Append(")");
                return sb.ToString();
            }

            sb.Append("MULTIPOLYGON (");

            List<Path> polygon = null;
            int polygonIndex = -1;
            foreach (var currentRing in Rings)
            {
                if (isEmptyRing(currentRing))
                    //skip empty rings because we can't determine if they are interior or exterior
                    //    - still an open question if this is the way to go
                    continue;

                //assumes the esri convention - a clockwise ring is an exterior ring, 
                //wkt and geojson have the opposite convention so before writing the string, coordinate lists are reversed
                //Any interior rings are ignored until the first exterior ring is detected.
                if (isClockwise(currentRing)) 
                {
                    if ((polygon != null) && (polygonIndex >= 0))
                    {
                        sb.Append("(");
                        sb.Append(String.Join(",", polygon.Select(x => x.CoordinatesText(true))));
                        sb.Append("),");
                    }
                    polygonIndex++;

                    //start a new polygon and add the ring
                    polygon = new List<Path> { currentRing };
                }
                else //add the inner ring
                {
                    if (polygon != null)
                        polygon.Add(currentRing);
                }

            }
            if (polygon != null)
            {
                sb.Append("(");
                sb.Append(String.Join(",", polygon.Select(x => x.CoordinatesText(true))));
                sb.Append("),");
            }
            var result = sb.ToString();
            result = result.TrimEnd(',');
            result += (")");

            return result;

            
        }

        private bool isEmptyRing(Path ring)
        {
            if (ring == null)
                return true;
            if (ring.Coordinates == null)
                return true;
            ring.Coordinates.RemoveAll(x => x == null);
            if (ring.Coordinates.Count == 0)
                return true;

            return false;
        }

        private bool isClockwise(Path ring)
        {
            if (ring == null)
                throw new ArgumentNullException("ring");

            List<Coordinate> coords = ring.Coordinates;
            if (coords == null)
                throw new InvalidOperationException("ring is empty");

            // a negative number indicates clockwise
            // esri convention is clockwise is an exterior ring, interior counter-clockwise. Geojson, wkt convention is opposite 
            double sum = 0;

            for (int i = 0; i < coords.Count - 1; i++)
            {
                double x1 = coords[i].x;
                double y1 = coords[i].y;

                double x2, y2;

                if (i == coords.Count - 2)
                {
                    x2 = coords[0].x;
                    y2 = coords[0].y;
                }
                else
                {
                    x2 = coords[i + 1].x;
                    y2 = coords[i + 1].y;
                }

                sum += ((x1 * y2) - (x2 * y1));
            }
            return sum < 0;
        }




    }
}
