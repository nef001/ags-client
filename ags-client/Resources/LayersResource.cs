using System;
using System.Collections.Generic;

using ags_client.Types;

namespace ags_client.Resources
{
    public class LayersResource : BaseResponse
    {
        public List<Layer> layers { get; set; }
        public List<Table> tables { get; set; }
    }
}
