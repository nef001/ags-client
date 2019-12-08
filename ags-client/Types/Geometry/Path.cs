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

        public override string ToString()
        {
            if ((Coordinates == null) || (Coordinates.Count == 0))
                return "LINESTRING EMPTY";

            var sb = new StringBuilder("LINESTRING(");
            sb.Append(String.Join(",", Coordinates.Select(x => x.ToString()).ToArray()));
            sb.Append(")");

            return sb.ToString();
        }

        public string CoordinatesText()
        {
            if ((Coordinates == null) || (Coordinates.Count == 0))
                return "EMPTY";
            if (Coordinates.Any(x => x == null))
            {
                throw new NullReferenceException("Geometry Path contains null coordinate(s).");
            }
            string commaSeparatedCoords = String.Join(",", Coordinates.Select(x => x.ToString()).ToArray());
            return $"({commaSeparatedCoords})";
        }
    }
}
