using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ags_client.Types
{
    public class ErrorDetail
    {
        public int code { get; set; }
        public string message { get; set; }
        public List<string> details { get; set; }
    }
}
