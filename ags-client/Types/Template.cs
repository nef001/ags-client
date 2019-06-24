using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ags_client.Types.Geometry;

namespace ags_client.Types
{
    public class Template<TA> 
        where TA : IRestAttributes
    {
        public string name { get; set; }
        public string description { get; set; }
        public TA prototype { get; set; } //??
    }
}
