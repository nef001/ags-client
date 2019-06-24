using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ags_client.Types
{
    public class Symbol
    {
        public string type { get; set; }
        public string style { get; set; }
        public int[] color { get; set; }
        public int? size { get; set; }
        public double? angle { get; set; }
        public int? xoffset { get; set; }
        public int? yoffset { get; set; }
    }
}
