﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RestSharp;
using Newtonsoft.Json;

using ags_client.Types;
using ags_client.Types.Geometry;
using ags_client.Resources.FeatureService;

namespace ags_client.Requests.MapService
{
    public class LayerQueryRequest<TF, TG, TA> : BaseRequest
        where TF : IRestFeature<TG, TA>
        where TG : IRestGeometry
        where TA : IRestAttributes
    {
    }
}
