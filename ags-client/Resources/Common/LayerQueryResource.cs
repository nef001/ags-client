﻿using ags_client.Types;
using ags_client.Types.Geometry;
using System.Collections.Generic;

namespace ags_client.Resources.Common
{
    public class LayerQueryResource<TF, TG, TA> : BaseResponse
        where TF : IRestFeature<TG, TA>
        where TG : IRestGeometry
        where TA : IRestAttributes
    {
        public string objectIdFieldName { get; set; }
        public string globalIdFieldName { get; set; }
        public string displayFieldName { get; set; }
        public List<Field> fields { get; set; }
        public string geometryType { get; set; }
        public SpatialReference spatialReference { get; set; }
        public bool? hasZ { get; set; }
        public bool? hasM { get; set; }
        public int? count { get; set; }
        public List<TF> features { get; set; }
        public List<int> objectids { get; set; }
    }

}
