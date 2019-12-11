using System;
using System.Collections.Generic;

namespace ags_client.Types.Geometry
{
    public class Envelope : IRestGeometry
    {
        public double? xMin { get; set; }
        public double? yMin { get; set; }
        public double? xMax { get; set; }
        public double? yMax { get; set; }
        public SpatialReference spatialReference { get; set; }

        public string ToWkt()
        {
            if ((!xMin.HasValue) || (!xMax.HasValue) || (!yMin.HasValue) || (!yMax.HasValue))
                return "POLYGON EMPTY";

            return new Polygon
            {
                spatialReference = this.spatialReference,
                Rings = new List<Path>
                {
                    new Path
                    {
                        Coordinates = new List<Coordinate>
                        {
                            new Coordinate { x = xMin.Value, y = yMin.Value },
                            new Coordinate { x = xMax.Value, y = yMin.Value },
                            new Coordinate { x = xMax.Value, y = yMax.Value },
                            new Coordinate { x = xMin.Value, y = yMax.Value },
                            new Coordinate { x = xMin.Value, y = yMin.Value },
                        }
                    }
                }
            }.ToWkt();
        }
    }
}
