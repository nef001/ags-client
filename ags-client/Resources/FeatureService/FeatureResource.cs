using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ags_client.Types;
using ags_client.Types.Geometry;


namespace ags_client.Resources.FeatureService
{
    public class FeatureResource<TF, TG, TA> : BaseResponse
        where TF : IRestFeature<TG, TA>
        where TG : IRestGeometry
        where TA : IRestAttributes
    {
        public TF feature { get; set; }
    }
}
