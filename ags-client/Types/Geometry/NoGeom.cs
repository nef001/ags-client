using System;

namespace ags_client.Types.Geometry
{
    public class NoGeom : IRestGeometry
    {
        public SpatialReference spatialReference { get; set; }

        public string ToWkt()
        {
            throw new NotSupportedException();
        }

        public override string ToString()
        {
            return "NONE";
        }
    }
}
