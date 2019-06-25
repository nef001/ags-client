using System;

using ags_client.Types;
using ags_client.Types.Geometry;
using ags_client.Resources.GeometryService;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ags_client.JsonConverters
{
    public class ConvexHullResourceConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(ConvexHullResource));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jObj = JObject.Load(reader);

            var result = new ConvexHullResource();
            if (jObj.ContainsKey("error"))
            {
                result.error = jObj["error"].ToObject<ErrorDetail>();
            }
            if (jObj.ContainsKey("geometryType"))
            {
                result.geometryType = (string)jObj["geometryType"];
                if (jObj.ContainsKey("geometry"))
                {
                    switch (result.geometryType)
                    {
                        case "esriGeometryPoint":
                            result.geometry = jObj["geometry"].ToObject<Point>();
                            break;
                        case "esriGeometryPolyline":
                            result.geometry = jObj["geometry"].ToObject<Polyline>();
                            break;
                        case "esriGeometryPolygon":
                            result.geometry = jObj["geometry"].ToObject<Polygon>();
                            break;
                        default:
                            break;
                    }
                }
            }
            return result;
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
