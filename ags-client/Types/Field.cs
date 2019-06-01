﻿

namespace ags_client.Types
{

    public class Field
    {
        public string name { get; set; }
        public string type { get; set; }
        public string alias { get; set; }
        public int? length { get; set; }
        public Domain domain { get; set; }
        public bool? required { get; set; }
    }
}
