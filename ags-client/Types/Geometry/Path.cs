using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.Serialization;

using Newtonsoft.Json;

namespace ags_client.Types.Geometry
{

    public class Path
    {
        [JsonIgnore]
        public List<Coordinate> Coordinates
        {
            get; set;
        }

        public double[][] ToArray()
        {
            if (Coordinates == null)
                return null;

            if (Coordinates.Count == 0)
                return null;

            double[][] result = new double[Coordinates.Count][];
            for (var i = 0; i < Coordinates.Count; i++)
            {
                result[i] = Coordinates.ElementAt(i).ToArray();
            }
            return result;
        }

        

        public string LineStringText(bool reversed)
        {
            /*
            < linestring text > ::= < empty set > | < left paren > 
                                    < point > 
                                    {< comma > < point >}* 
                                    < right paren >
            */

            List<Coordinate> coords = Coordinates;
            if (reversed) coords = reverseCoordinates();
            if (coords == null)
                return "EMPTY";
            coords.RemoveAll(x => x == null);
            if (coords.Count == 0)
                return "EMPTY";
            
            return $"({String.Join(",", coords.Select(x => x.PointString()).ToArray())})";
        }

        public string LineStringTaggedText(bool reversed)
        {
            /*
             <linestring tagged text> ::= linestring <linestring text>
             */

            return $"LINESTRING {LineStringText(reversed)}";
        }

        private List<Coordinate> reverseCoordinates()
        {
            if (Coordinates == null)
                return null;

            if (Coordinates.Count < 2)
                return Coordinates;

            return Coordinates.AsEnumerable().Reverse().ToList();
        }

        public double SignedArea() // assumes a ring - a closed path where first and last coordinates are the same
        {
            if (Coordinates == null)
                return 0;

            var nonNullCoords = (Coordinates.Where(x => x != null)).ToList();

            if (nonNullCoords.Count < 2)
                throw new InvalidOperationException("Path is not a closed ring");

            if (nonNullCoords[0].Equals(nonNullCoords.Last()) == false)
                throw new InvalidOperationException("Path is not a closed ring");

            //need 4 coords for a non-empty ring
            if (nonNullCoords.Count < 4)
                return 0;

            double sum = 0;

            for (int i = 0; i < Coordinates.Count - 1; i++)
            {
                double x1 = Coordinates[i].x;
                double y1 = Coordinates[i].y;

                double x2, y2;

                if (i == Coordinates.Count - 2)
                {
                    x2 = Coordinates[0].x;
                    y2 = Coordinates[0].y;
                }
                else
                {
                    x2 = Coordinates[i + 1].x;
                    y2 = Coordinates[i + 1].y;
                }

                sum += ((x1 * y2) - (x2 * y1));
            }
            return sum;
        }

        //Orders coordinates by the angle from the "mean" coord.
        //Null coordinates removed.
        public List<Coordinate> OrientPath()
        {
            if (Coordinates == null)
                return null;

            var nonNullCoords = (Coordinates.Where(c => c != null));
            var nonUnique = new List<Coordinate>();
            var unique = new HashSet<Coordinate>();

            foreach (var c in nonNullCoords)
                if (!unique.Add(c)) nonUnique.Add(c);

            if (unique.Count == 0) // don't bother
                return nonNullCoords.ToList();

            var cx = unique.Average(c => c.x);
            var cy = unique.Average(c => c.y);

            var ordered = unique.OrderBy(c => Math.Atan2(c.y - cy, c.x - cx)).ToList();

            //re-arrange to put the start point first and last
            var arranged = new List<Coordinate>();
            if (nonUnique.Count > 0)
            {
                int startIndex = ordered.IndexOf(nonUnique[0]);

                var startRange = ordered.Skip(startIndex);
                var endRange = ordered.Take(startIndex);

                arranged.AddRange(startRange);
                arranged.AddRange(endRange);
                arranged.Add(nonUnique[0]);

                return arranged;
            }

            return ordered;
        }

    }
}
