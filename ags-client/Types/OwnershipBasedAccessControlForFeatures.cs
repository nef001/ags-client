using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ags_client.Types
{
    public class OwnershipBasedAccessControlForFeatures
    {
        public bool? allowOthersToUpdate { get; set; }
        public bool? allowOthersToDelete { get; set; }
        public bool? allowOthersToQuery { get; set; }
    }
}
