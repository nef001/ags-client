using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ags_client.Types;

namespace ags_client.Resources
{
    public class LayerOrTableResource : BaseResponse
    {
        public int id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string geometryType { get; set; }
        public List<Field> fields { get; set; }


    }
}
