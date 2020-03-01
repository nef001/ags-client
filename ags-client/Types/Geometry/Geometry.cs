namespace ags_client.Types.Geometry
{
    public class Geometry<TG>
        where TG : IRestGeometry
    {
        public string geometryType { get; set; }
        public TG geometry { get; set; }
    }
}
