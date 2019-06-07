using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RestSharp;
using Newtonsoft.Json;

using ags_client.Types.Geometry;

namespace ags_client.Operations.GeometryOps
{
    public class DensifyOp<TG>
        where TG : IRestGeometry
    {
        public Geometries<TG> geometries { get; set; }
    }
}
