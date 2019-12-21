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
            else // treat as MULTIPOLYGON
            {
                sb.Append("MULTIPOLYGON "); 
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
                    //if Rings[0] is interior or empty, skip the polygon
                    if (polygon.Rings[0].SignedArea() >= 0)
                        continue;

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

        
        private List<Polygon> multiPolygonToList()
        {
            var result = new List<Polygon>();

            if ((Rings == null) || (Rings.Count == 0))
                return result;

            //get the indices of exterior rings
            var exteriorRingIndices = new List<int>();
            for (int i = 0; i < Rings.Count; i++)
                if (Rings[i].SignedArea() < 0)
                    exteriorRingIndices.Add(i);

            if (exteriorRingIndices.Count == 0) // no exterior rings -return a new single ring polygon for each interior or empty ring
            {
                foreach (var ring in Rings)
                    result.Add(new Polygon { Rings = new List<Path> { ring } });
                return result;
            }

            //every interior before the first exterior becomes a new polygon
            if (exteriorRingIndices[0] > 0)
            {
                for (int i=0; i<exteriorRingIndices[0]; i++)
                {
                    result.Add(new Polygon { Rings = new List<Path> { Rings[i] } });
                }
            }

            //now each new Exterior starts a new polygon that includes subsequent interior or empty rings
            for (int i = 0; i < exteriorRingIndices.Count; i++)
            {
                int exteriorIndx = exteriorRingIndices[i];

                var polygon = new Polygon { Rings = new List<Path> { this.Rings[exteriorIndx] } };
                if (this.Rings.Count > (exteriorIndx + 1))
                {
                    int takenum;
                    if ((i + 1) < exteriorRingIndices.Count)
                    {
                        takenum = exteriorRingIndices[i + 1] - exteriorRingIndices[i];
                    }
                    else //take aany remaining
                        takenum = Rings.Count - (exteriorIndx + 1);

                    var interiorRings = Rings.Skip(exteriorIndx + 1).Take(takenum);
                    polygon.Rings.AddRange(interiorRings);
                }
                result.Add(polygon);
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
