using System.Collections.Generic;

using ags_client.Types.Geometry;

namespace ags_client.Resources.GeometryService
{
    public class LabelPointsResource : BaseResponse
    {
        public List<Point> labelPoints { get; set; }
    }
}
