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

        public override string ToString()
        {
            return ToWkt();
        }

        public string ToWkt() //only correct for empty or single Ring polygon or a polygon with outer and inner ring
        {
            if (Rings == null)
                return "POLYGON EMPTY";

            Rings.RemoveAll(x => x == null);

            if (Rings.Count == 0)
                return "POLYGON EMPTY";

            if (Rings.Count > 2)
                return "MULTIPOLYGON EMPTY"; // multipolygons not supported

            var sb = new StringBuilder();

            sb.Append("POLYGON (");
            sb.Append(String.Join(",", Rings.Select(x => x.CoordinatesText())));
            sb.Append(")");

            return sb.ToString();
        }


/*
        private void FindOrientationSimple(Polygon PolyToCheck)
        {
            foreach (var part in PolyToCheck.Parts)
            {
                // construct a list of ordered coordinate pairs
                List<Coordinate> ringCoordinates = new List<Coordinate>(PolyToCheck.PointCount);

                foreach (var segment in part)
                {
                    ringCoordinates.Add(segment.StartCoordinate);
                    ringCoordinates.Add(segment.EndCoordinate);
                }

                // this is not the true area of the part
                // a negative number indicates an outer ring and a positive number represents an inner ring
                // (this is the opposite from the ArcGIS.Core.Geometry understanding)
                double signedArea = 0;

                // for all coordinates pairs compute the area
                // the last coordinate needs to reach back to the starting coordinate to complete
                for (int cIndex = 0; cIndex < ringCoordinates.Count - 1; cIndex++)
                {
                    double x1 = ringCoordinates[cIndex].X;
                    double y1 = ringCoordinates[cIndex].Y;

                    double x2, y2;

                    if (cIndex == ringCoordinates.Count - 2)
                    {
                        x2 = ringCoordinates[0].X;
                        y2 = ringCoordinates[0].Y;
                    }
                    else
                    {
                        x2 = ringCoordinates[cIndex + 1].X;
                        y2 = ringCoordinates[cIndex + 1].Y;
                    }

                    signedArea += ((x1 * y2) - (x2 * y1));
                }

                // if signedArea is a negative number => indicates an outer ring 
                // if signedArea is a positive number => indicates an inner ring
                // (this is the opposite from the ArcGIS.Core.Geometry understanding)

            }
        }*/


    }
}
