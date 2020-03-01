
using ags_client.Types.Geometry;

namespace ags_client.Types
{
    public class CommonF<TG> : IRestFeature<TG, CommonAttributes>
        where TG : IRestGeometry
    {
        public TG geometry { get; set; }
        public CommonAttributes attributes { get; set; }
    }
}
