using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ags_client.Types
{
    public class Renderer
    {
        public string type { get; set; }
        public Symbol symbol { get; set; }
        public string label { get; set; }
        public string description { get; set; }
        public string rotationType { get; set; }
        public string rotationExpression { get; set; }
        public List<UniqueValueInfo> uniqueValueInfos { get; set; }
    }
}
