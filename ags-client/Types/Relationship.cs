using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ags_client.Types
{
    public class Relationship
    {
        public int? id { get; set; }
        public string name { get; set; }
        public int? relatedTableId { get; set; }
        public string role { get; set; }
        public string cardinality { get; set; }
        public string keyField { get; set; }
        public bool? composite { get; set; }
        public int? relationshipTableId { get; set; }
        public string keyFieldInRelationshipTable { get; set; }
    }
}
