using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ags_client.Types;

namespace ags_client.Resources
{
    public class CatalogResource : BaseResponse
    {
        public string currentVersion { get; set; }
        public List<string> folders { get; set; }
        public List<Service> services { get; set; }
    }
}
