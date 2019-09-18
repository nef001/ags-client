using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ags_client.Resources
{
    public class GenerateTokenResource : BaseResponse
    {
        public string token { get; set; }
        public long expires { get; set; }
    }
}
