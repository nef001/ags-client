using ags_client.Types.Geometry;
using System.Collections.Generic;

namespace ags_client.Resources.GeometryService
{
    public class RelationResource : BaseResponse
    {
        public List<GeometryRelation> relations { get; set; }
    }
}
