

using ags_client.Types.Geometry;

namespace ags_client.Types
{
    public interface IRestFeature<TG, TA>
        where TG : IRestGeometry
        where TA : IRestAttributes
    {
        TG geometry { get; set; }
        TA attributes { get; set; }
    }
}
