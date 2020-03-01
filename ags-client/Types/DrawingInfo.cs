using System.Collections.Generic;

namespace ags_client.Types
{
    public class DrawingInfo
    {
        public Renderer renderer { get; set; }
        public int? transparency { get; set; }
        public List<LabelClass> labellingInfo { get; set; }
    }
}
