using ags_client.JsonConverters;
using Newtonsoft.Json;
using System;

namespace ags_client.Resources
{
    public class GenerateTokenResource : BaseResponse
    {
        public string token { get; set; }

        [JsonConverter(typeof(DateTimeUnixConverter))]
        public DateTime? expires { get; set; }
    }
}
