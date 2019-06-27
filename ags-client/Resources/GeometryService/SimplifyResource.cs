﻿using System.Collections.Generic;

using ags_client.Types.Geometry;

namespace ags_client.Resources.GeometryService
{
    public class SimplifyResource<TG> : BaseResponse
        where TG : IRestGeometry
    {
        public List<TG> geometries { get; set; }
    }
}