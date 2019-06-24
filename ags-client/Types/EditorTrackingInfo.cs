using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ags_client.Types
{
    public class EditorTrackingInfo
    {
        public bool enableEditorTracking { get; set; }
        public bool enableOwnershipAccessControl { get; set; }
        public bool allowOthersToUpdate { get; set; }
        public bool allowOthersToDelete { get; set; }
    }
}
