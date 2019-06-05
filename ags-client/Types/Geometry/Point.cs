

namespace ags_client.Types.Geometry
{
    public class Point : Coordinate, IRestGeometry
    {
        //public Point() { geometryType = "esriGeometryPoint"; }
        //public string geometryType { get; set; }
        public SpatialReference spatialReference { get; set; }

        
    }
}
