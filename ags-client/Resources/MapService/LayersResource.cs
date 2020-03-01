using ags_client.Types;
using System.Collections.Generic;

namespace ags_client.Resources.MapService
{
    public class LayersResource : BaseResponse
    {
        public List<Layer> layers { get; set; }
        public List<Table> tables { get; set; }
    }
}
