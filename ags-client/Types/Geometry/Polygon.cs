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



        public string ToWkt()
        {
            if (Rings == null)
                return "POLYGON EMPTY";

            if (Rings.Count == 0)
                return "POLYGON EMPTY";

            // Count the clockwise non-empty rings (esri exterior)
            var nonEmptyRings = Rings.Where(x => isEmptyRing(x) == false);
            var exteriorRings = nonEmptyRings.Where(x => signedArea(x) < 0).ToList();

            if (exteriorRings.Count == 0)
                return "POLYGON EMPTY";

            var sb = new StringBuilder();
            if (exteriorRings.Count == 1) // treat as POLYGON and assume Ring[0} is exterior.
            {
                sb.Append("POLYGON ");
                sb.Append(polygonText(Rings));
                return sb.ToString();
            }

            sb.Append("MULTIPOLYGON ");

            List<string> polygonStrings = new List<string>();
            List<Path> rings = null;
            foreach (var currentRing in Rings)
            {
                //if (isEmptyRing(currentRing))
                //    //skip empty rings because we can't determine if they are interior or exterior
                //    //    - still an open question if this is the way to go
                //    continue;


                //Any interior rings are ignored until the first exterior ring is detected.
                if (signedArea(currentRing) < 0) 
                {
                    if (rings != null)
                    {
                        polygonStrings.Add(polygonText(rings));
                    }
                    

                    //start a new set of rings and add the current ring
                    rings = new List<Path> { currentRing };
                }
                else //add the inner ring to the current ring set
                {
                    if (rings != null)
                        rings.Add(currentRing);
                }

            }
            if (rings != null)
            {
                //finally add the last ring set polygon strings
                polygonStrings.Add(polygonText(rings));
            }

            //join up the text
            sb.Append($"({String.Join(",", polygonStrings)})");

            return sb.ToString();

        }

        private string polygonText(List<Path> rings)
        {
            /*
            < polygon text > ::= < empty set > | 
                            < left paren > < linestring text > {< comma > < linestring text >}* < right paren >
            */
            if (rings == null)
                return "EMPTY";
            if (rings.Count == 0)
                return "EMPTY";

            return $"({String.Join(",", rings.Select(x => x.LineStringText(true)).ToArray())})";
        }

        private string multipolygontext()
        {
            /*
            < multipolygon text > ::=  < empty set > | 
                            < left paren > < polygon text > {< comma > < polygon text >}* < right paren >
            */
            return "";
        }

        private bool isEmptyRing(Path ring)
        {
            if (ring == null)
                return true;
            if (ring.Coordinates == null)
                return true;
            if (ring.Coordinates.Where(x => x == null).Any())
                return true;
            if (ring.Coordinates.Count == 0)
                return true;

            if (Math.Abs(signedArea(ring)) < double.Epsilon)
                return true;

            return false;
        }

        /// <summary>
        /// A negative number indicates clockwise ring. 0 indicates empty? Positive indicates anti-clockwise.
        /// Esri convention is clockwise is an exterior ring, interior counter-clockwise. Geojson, wkt convention is opposite 
        /// </summary>
        /// <param name="ring"></param>
        /// <returns></returns>
        private double signedArea(Path ring)
        {
            if (ring == null)
                return 0;

            List<Coordinate> coords = ring.Coordinates;
            if (coords == null)
                return 0;

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
            return sum;
        }


        

        

    }
}
