using ags_client.Types;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ags_client.Resources.MapService
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



        public int LayerIdByName(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Empty parameter.", "name");
            if ((layers == null) || (layers.Count == 0))
                throw new Exception("MapServiceResource has null or empty layers list.");
            var layer = layers.Where(x => x.name.Equals(name, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            if (layer == null)
                throw new Exception($"Layer: [{name}] not found.");
            return layer.id;
        }
        public int TableIdByName(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Empty parameter.", "name");
            if ((tables == null) || (tables.Count == 0))
                throw new Exception("MapServiceResource has null or empty tables list.");
            var table = tables.Where(x => x.name.Equals(name, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            if (table == null)
                throw new Exception($"Table: [{name}] not found.");
            return table.id;
        }

    }
}
