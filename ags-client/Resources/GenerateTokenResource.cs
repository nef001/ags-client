using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ags_client.JsonConverters;
using Newtonsoft.Json;

namespace ags_client.Resources
{
    public class GenerateTokenResource : BaseResponse
    {
        public string token { get; set; }

        [JsonConverter(typeof(DateTimeUnixConverter))]
        public DateTime? expires { get; set; }
    }
}
