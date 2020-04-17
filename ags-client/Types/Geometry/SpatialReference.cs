namespace ags_client.Types.Geometry
{
    public class SpatialReference
    {
        public int? wkid { get; set; }
        public int? latestWkid { get; set; }
        public int? vcsWkid { get; set; }
        public int? latestVcsWkid { get; set; }
        public string wkt { get; set; }
    }
}
