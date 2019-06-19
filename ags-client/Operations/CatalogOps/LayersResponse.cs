using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ags_client.Types;

namespace ags_client.Operations.CatalogOps
{
    public class LayersResponse : BaseResponse
    {
        public List<Layer> layers { get; set; }
        public List<Table> tables { get; set; }
    }
}
