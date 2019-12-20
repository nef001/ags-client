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
            var multiList = multiPolygonToList();

            if (multiList.Count == 0)
                return "POLYGON EMPTY";

            var sb = new StringBuilder();
            if (multiList.Count == 1)  // treat as POLYGON
            {
                sb.Append("POLYGON ");
                sb.Append(polygonText(multiList[0].Rings));
            }
            else
            {
                sb.Append("MULTIPOLYGON "); // treat as MULTIPOLYGON
                sb.Append($"({String.Join(",", multiList.Select(polygon => polygonText(polygon.Rings)))})");
            }
            return sb.ToString();
        }


        


        public bool PointInPoly(Coordinate p)
        {
            var multipolygon = multiPolygonToList();
            if ((multipolygon == null) || (multipolygon.Count == 0))
                return false;

            bool inCurrent; 
            foreach (var polygon in multipolygon)
            {
                if ((polygon.Rings == null) || (polygon.Rings.Count == 0))
                {
                    inCurrent = false;
                }
                else
                {
                    if (polygon.Rings[0].ContainsPoint(p) != 0) // 1 = inside, -1 = on the boundary, 0 outside
                    {
                        inCurrent = true;
                        if (polygon.Rings.Count > 1)
                        {
                            for (int i = 1; i < polygon.Rings.Count; i++)
                            {
                                if (polygon.Rings[i].ContainsPoint(p) == 1)
                                {
                                    inCurrent = false;
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        inCurrent = false;
                    }
                }
                if (inCurrent)
                    return true;
            }
            return false;
        }

        // converts this to a list of polgons each with a single exterior ring and 0 or more interior rings
        // the exterior ring will be the first path in each polygon's "Rings" path list
        // returned polygon.Rings is never null
        // assumes esri ordering and maintains it
        // excludes any unclosed rings and empty exterior rings
        // will include empty interior rings that are found after the first valid exterior
        // 
        // Needs improved handling of empty rings
        //
        private List<Polygon> multiPolygonToList()
        {
            var result = new List<Polygon>();

            if ((Rings == null) || (Rings.Count == 0))
                return result;

            var closedPolygonRings = Rings.Where(ring => ring.IsClosedRing());

            List<Path> rings = null;
            foreach (var ring in closedPolygonRings)
            {
                if (ring.SignedArea() < 0) //esri outer ring
                {
                    if (rings != null)
                    {
                        result.Add(new Polygon { Rings = rings });
                    }
                    rings = new List<Path> { ring };
                }
                else //add the inner (or empty) ring to the current ring set
                {
                    if (rings != null)
                        rings.Add(ring);
                }
            }
            if (rings != null)
            {
                //finally add the last ring set polygon 
                result.Add(new Polygon { Rings = rings });
            }

            return result;
        }


        private string polygonText(List<Path> rings)
        {
            /*
            < polygon text > ::= < empty set > | 
                            < left paren > < linestring text > {< comma > < linestring text >}* < right paren >
            */
            if ((rings == null) || (rings.Count == 0))
                return "EMPTY";

            return $"({String.Join(",", rings.Select(x => x.LineStringText(true)))})";
        }



        


    }
}
