﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ags_client.Types.Geometry;

namespace ags_client.Operations.GeometryOps
{
    public class AutoCompleteResponse : BaseResponse
    {
        public List<Polygon> geometries { get; set; } //polygons
        public SpatialReference spatialReference { get; set; }

    }
}