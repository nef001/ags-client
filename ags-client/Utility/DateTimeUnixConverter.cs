﻿using System;
using Newtonsoft.Json;

namespace ags_client.Utility
{
    /* ArcGIS REST API formats datetime values in json as milliseconds since the Unix epoch 1/1/1970.
       This custom converter can be used to convert back and forth.

       Usage as a property attribute:
       
        public class VehicleA :IRestAttributes
        {
            public int objectid { get; set; }
            public int? node_id { get; set; }

            [JsonConverter(typeof(DateTimeUnixConverter))]
            public DateTime? date_time { get; set; }

            public string description { get; set; }
            public string type_descr { get; set; }
        }
         
    */
    public class DateTimeUnixConverter :JsonConverter<DateTime>
    {
        static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1);
        public override void WriteJson(JsonWriter writer, DateTime value, JsonSerializer serializer)
        {
            long unixMilliseconds = (long)value.Subtract(UnixEpoch).TotalMilliseconds;
            writer.WriteValue(unixMilliseconds);
        }

        public override DateTime ReadJson(JsonReader reader, Type objectType, DateTime existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            long unixMilliseconds = (long)reader.Value;
            return UnixEpoch.AddMilliseconds(unixMilliseconds);
        }
    }
}
