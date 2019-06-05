﻿using System.Collections.Generic;

using ags_client.Types;
using ags_client.Types.Geometry;

namespace ags_client.Operations.LayerOps
{
    public class LayerQueryResponse<TF, TG, TA> : BaseResponse
        where TF : IRestFeature<TG, TA>
        where TG : IRestGeometry
        where TA : IRestAttributes
    {
        public string displayFieldName { get; set; }
        public Dictionary<string, string> fieldAliases { get; set; }
        public List<Field> fields { get; set; }
        public string geometryType { get; set; }
        public SpatialReference spatialReference { get; set; }
        public List<TF> features { get; set; }
    }

}