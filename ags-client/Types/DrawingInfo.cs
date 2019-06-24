using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ags_client.Types
{
    public class DrawingInfo
    {
        public Renderer renderer { get; set; }
        public int? transparency { get; set; }
        public List<LabelClass> labellingInfo { get; set; }
    }
}
