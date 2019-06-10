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
        [JsonProperty("paths")]
        private double[][][] _pathsArray;

        [JsonIgnore]
        public List<PointArray> Paths { get; set; }

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

        [OnSerializing]
        internal void OnSerializing(StreamingContext context)
        {
            _pathsArray = ToArray();
        }

        [OnDeserialized]
        internal void OnDeserialized(StreamingContext context)
        {
            Paths = new List<PointArray>();
            foreach (var currentPathPointList in _pathsArray.Select(
                path => path.Select(
                    point => new Coordinate { x = point[0], y = point[1] }).ToList()))
            {
                Paths.Add(new PointArray { Points = currentPathPointList });
            }

            if (Paths.Any(x => x == null)) { throw new InvalidOperationException("The collection may not contain a null path."); }
        }
    }
}
