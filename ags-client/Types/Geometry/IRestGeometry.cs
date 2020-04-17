namespace ags_client.Types.Geometry
{
    public interface IRestGeometry
    {
        SpatialReference spatialReference { get; set; }
        string ToWkt();
    }
}
