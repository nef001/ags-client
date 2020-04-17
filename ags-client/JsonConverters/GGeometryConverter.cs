using ags_client.Types.Geometry;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace ags_client.JsonConverters
{
    public class GGeometryConverter : JsonConverter<GGeometry>
    {
        const string gtKey = "geometryType";
        const string gmKey = "geometry";

        public override GGeometry ReadJson(JsonReader reader, Type objectType, GGeometry existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JObject jObj = JObject.Load(reader);

            GGeometry result = null;
            if (jObj.ContainsKey(gtKey))
            {
                result = new GGeometry
                {
                    geometryType = (string)jObj[gtKey]
                };
                if (jObj.ContainsKey(gmKey))
                {
                    switch (result.geometryType)
                    {
                        case GeometryHelper.POINT_GEOMETRY:
                            result.geometry = jObj[gmKey].ToObject<Point>();
                            break;
                        case GeometryHelper.POLYLINE_GEOMETRY:
                            result.geometry = jObj[gmKey].ToObject<Polyline>();
                            break;
                        case GeometryHelper.POLYGON_GEOMETRY:
                            result.geometry = jObj[gmKey].ToObject<Polygon>();
                            break;
                        case GeometryHelper.MULTIPOINT_GEOMETRY:
                            result.geometry = jObj[gmKey].ToObject<MultiPoint>();
                            break;
                        case GeometryHelper.ENVELOPE_GEOMETRY:
                            result.geometry = jObj[gmKey].ToObject<Envelope>();
                            break;
                        default:
                            break;
                    }
                }
            }
            return result;
        }

        public override void WriteJson(JsonWriter writer, GGeometry value, JsonSerializer serializer)
        {
            if (value == null)
                return;
            JObject jObj = new JObject();
            if (!String.IsNullOrWhiteSpace(value.geometryType))
            {
                jObj.Add(gtKey, value.geometryType);
                if (value.geometry != null)
                {
                    switch (value.geometryType)
                    {
                        case GeometryHelper.POINT_GEOMETRY:
                            jObj.Add(gmKey, JToken.FromObject((Point)value.geometry));
                            break;
                        case GeometryHelper.POLYLINE_GEOMETRY:
                            jObj.Add(gmKey, JToken.FromObject((Polyline)value.geometry));
                            break;
                        case GeometryHelper.POLYGON_GEOMETRY:
                            jObj.Add(gmKey, JToken.FromObject((Polygon)value.geometry));
                            break;
                        case GeometryHelper.MULTIPOINT_GEOMETRY:
                            jObj.Add(gmKey, JToken.FromObject((MultiPoint)value.geometry));
                            break;
                        case GeometryHelper.ENVELOPE_GEOMETRY:
                            jObj.Add(gmKey, JToken.FromObject((Envelope)value.geometry));
                            break;
                        default:
                            break;
                    }
                }
            }
            writer.WriteValue(jObj.ToString());
        }
    }
}
