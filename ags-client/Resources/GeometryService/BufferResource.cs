﻿using ags_client.Types.Geometry;
using System.Collections.Generic;

namespace ags_client.Resources.GeometryService
{
    public class BufferResource<TG> : BaseResponse
        where TG : IRestGeometry
    {
        public List<TG> geometries { get; set; }
    }
}
