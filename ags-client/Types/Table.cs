using System.Collections.Generic;

namespace ags_client.Types
{
    public class Table
    {
        public int id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public List<Field> fields { get; set; }
    }
}
