
using ags_client.Types;
using ags_client.Types.Geometry;


namespace ags_client.Resources.Common
{
    public class FeatureResource<TF, TG, TA> : BaseResponse
        where TF : IRestFeature<TG, TA>
        where TG : IRestGeometry
        where TA : IRestAttributes
    {
        public TF feature { get; set; }
    }
}
