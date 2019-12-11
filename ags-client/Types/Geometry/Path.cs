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

            var sb = new StringBuilder("LINESTRING ");
            sb.Append(CoordinatesText());

            return sb.ToString();
        }

        /// <summary>
        /// Returns the bracketed coordinate list or EMPTY
        /// </summary>
        /// <param name="reverse">use to reverse the orientation of rings in polygons</param>
        /// <returns></returns>
        public string CoordinatesText(bool reverse = false)
        {
            if (Coordinates == null)
                return "EMPTY";

            Coordinates.RemoveAll(x => x == null);

            if (Coordinates.Count == 0)
                return "EMPTY";

            string[] coords;
            if (reverse)
                coords = Coordinates.Select(x => x.ToString()).Reverse().ToArray();
            else
                coords = Coordinates.Select(x => x.ToString()).ToArray();

            string commaSeparatedCoords = String.Join(",", coords);
            return $"({commaSeparatedCoords})";
        }
    }
}
