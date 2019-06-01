using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.Serialization;

using Newtonsoft.Json;

namespace ags_client.Types.Geometry
{

    public class PointArray
    {
        [JsonIgnore]
        public List<Coordinate> Points
        {
            get; set;
        }

        public double[][] ToArray()
        {
            if (Points == null)
                return null;
            if (Points.Count == 0)
                return null;

            double[][] result = new double[Points.Count][];
            for (var i = 0; i < Points.Count; i++)
            {
                result[i] = Points.ElementAt(i).ToArray();
            }
            return result;
        }
    }
}
