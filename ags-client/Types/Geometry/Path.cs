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

            Coordinates.RemoveAll(x => x == null);

            if (Coordinates.Count == 0)
                return null;

            double[][] result = new double[Coordinates.Count][];
            for (var i = 0; i < Coordinates.Count; i++)
            {
                result[i] = Coordinates.ElementAt(i).ToArray();
            }
            return result;
        }

        //public string ToWkt()
        //{
        //    if (Coordinates == null)
        //        return "LINESTRING EMPTY";

        //    Coordinates.RemoveAll(x => x == null);

        //    if  (Coordinates.Count == 0)
        //        return "LINESTRING EMPTY";

        //    var sb = new StringBuilder("LINESTRING ");
        //    sb.Append(this);

        //    return sb.ToString();
        //}

        public void Reverse()
        {
            if (Coordinates == null)
                return;

            Coordinates.RemoveAll(x => x == null);

            if (Coordinates.Count < 2)
                return;

            Coordinates.Reverse();
        }

        /// <summary>
        /// Returns the bracketed coordinate list or EMPTY
        /// </summary>
        public override string ToString()
        {
            if (Coordinates == null)
                return "EMPTY";

            Coordinates.RemoveAll(x => x == null);

            if (Coordinates.Count == 0)
                return "EMPTY";

            string[] coords = Coordinates.Select(x => x.ToString()).ToArray();

            string commaSeparatedCoords = String.Join(",", coords);
            return $"({commaSeparatedCoords})";
        }

    }
}
