

namespace ags_client.Types.Geometry
{
    public class Point : Coordinate, IRestGeometry
    {
        public SpatialReference spatialReference { get; set; }

        

        public string ToWkt()
        {
            return $"POINT ({this})";
        }
    }
}
