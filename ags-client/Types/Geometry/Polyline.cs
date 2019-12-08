using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.Serialization;

using Newtonsoft.Json;

namespace ags_client.Types.Geometry
{

    public class Polyline : IRestGeometry
    {
        public SpatialReference spatialReference { get; set; }

        [JsonProperty("paths")]
        private double[][][] _pathsArray;

        [JsonIgnore]
        public List<Path> Paths { get; set; }

        public double[][][] ToArray()
        {
            if (Paths == null)
                return null;
            if (Paths.Count == 0)
                return null;


            double[][][] result = new double[Paths.Count][][];
            for (var i = 0; i < Paths.Count; i++)
            {
                result[i] = Paths.ElementAt(i).ToArray();
            }
            return result;
        }

        public string ToWkt()
        {
            if ((Paths == null) || (Paths.Count == 0))
                return "LINESTRING EMPTY";
            if (Paths.Count == 1)
                return Paths[0].ToString();

            var sb = new StringBuilder("MULTILINESTRING(");
            sb.Append(String.Join(",", Paths.Select(x => x.CoordinatesText())));
            sb.Append(")");
            return sb.ToString();
        }

        public override string ToString()
        {
            return ToWkt();
        }


        [OnSerializing]
        internal void OnSerializing(StreamingContext context)
        {
            _pathsArray = ToArray();
        }

        [OnDeserialized]
        internal void OnDeserialized(StreamingContext context)
        {
            Paths = new List<Path>();
            foreach (var currentPathPointList in _pathsArray.Select(
                path => path.Select(
                    point => new Coordinate { x = point[0], y = point[1] }).ToList()))
            {
                Paths.Add(new Path { Coordinates = currentPathPointList });
            }

            if (Paths.Any(x => x == null)) { throw new InvalidOperationException("The collection may not contain a null path."); }
        }
    }
}
