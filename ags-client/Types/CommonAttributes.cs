using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ags_client.JsonConverters;
using Newtonsoft.Json;

namespace ags_client.Types
{
    public class CommonAttributes : IRestAttributes
    {
        public int objectid { get; set; }
        public string globalid { get; set; }
        public string tag { get; set; }
        public int? pin { get; set; }
        public int? fl { get; set; }
        public int address_key { get; set; }
        public string createuserid { get; set; }
        public string updateuserid { get; set; }

        [JsonConverter(typeof(DateTimeUnixConverter))]
        public DateTime? createdate { get; set; }

        [JsonConverter(typeof(DateTimeUnixConverter))]
        public DateTime? updatedate { get; set; }
    }
}
