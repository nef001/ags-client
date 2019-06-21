using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ags_client.Types;

namespace ags_client.Resources
{
    public class MapServiceResource : BaseResponse
    {
        public string currentVersion { get; set; }
        public string serviceDescription { get; set; }
        public string mapName { get; set; }
        public string description { get; set; }
        public string copyrightText { get; set; }
        public bool? supportsDynamicLayers { get; set; }
        public List<Layer> layers { get; set; }
        public List<Table> tables { get; set; }

        //incomplete

    }
}
