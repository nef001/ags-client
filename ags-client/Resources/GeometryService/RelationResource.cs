using System.Collections.Generic;

using ags_client.Types.Geometry;

namespace ags_client.Resources.GeometryService
{
    public class RelationResource : BaseResponse
    {
        public List<GeometryRelation> relations { get; set; }
    }
}
