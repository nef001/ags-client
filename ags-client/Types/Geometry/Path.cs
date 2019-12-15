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

        /// <summary>
        /// Returns the bracketed coordinate list or EMPTY
        /// </summary>
        //public override string ToString()
        //{
        //    if (Coordinates == null)
        //        return "EMPTY";

        //    Coordinates.RemoveAll(x => x == null);

        //    if (Coordinates.Count == 0)
        //        return "EMPTY";

        //    string[] coords = Coordinates.Select(x => x.ToString()).ToArray();

        //    string commaSeparatedCoords = String.Join(",", coords);
        //    return $"({commaSeparatedCoords})";
        //}

    }
}
