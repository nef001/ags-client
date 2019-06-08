﻿using System.Collections.Generic;

using ags_client.Types.Geometry;

namespace ags_client.Operations.GeometryOps
{
    public class BufferResponse<TG> : BaseResponse
        where TG : IRestGeometry
    {
        public List<TG> geometries { get; set; }
    }
}