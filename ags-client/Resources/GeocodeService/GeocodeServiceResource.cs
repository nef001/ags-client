using ags_client.Types;
using ags_client.Types.Geometry;
using System.Collections.Generic;

namespace ags_client.Resources.GeocodeService
{
    public class GeocodeServiceResource : BaseResponse
    {
        public string currentVersion { get; set; }
        public string serviceDescription { get; set; }
        public string capabilities { get; set; }
        public List<Field> addressFields { get; set; }
        public Field singleLineAddressField { get; set; }
        public List<Field> candidateFields { get; set; }
        public List<Field> intersectionCandidateFields { get; set; }
        public SpatialReference spatialReference { get; set; }
        public Dictionary<string, object> locatorProperties { get; set; }
        public List<Name> locators { get; set; }
    }
}
