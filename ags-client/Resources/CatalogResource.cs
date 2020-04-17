using ags_client.Types;
using System.Collections.Generic;

namespace ags_client.Resources
{
    public class CatalogResource : BaseResponse
    {
        public string currentVersion { get; set; }
        public List<string> folders { get; set; }
        public List<Service> services { get; set; }
    }
}
