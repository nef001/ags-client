using ags_client.Types.Geometry;
using System.Collections.Generic;

namespace ags_client.Resources.GeometryService
{
    public class LabelPointsResource : BaseResponse
    {
        public List<Point> labelPoints { get; set; }
    }
}
