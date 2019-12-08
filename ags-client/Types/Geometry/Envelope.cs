using System;


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
            throw new NotImplementedException();
        }
    }
}
