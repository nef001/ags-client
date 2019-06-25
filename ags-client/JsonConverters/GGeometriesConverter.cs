using System;
using System.Linq;
using System.Collections.Generic;

using ags_client.Types;
using ags_client.Types.Geometry;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ags_client.JsonConverters
{
    public class GGeometriesConverter : JsonConverter<GGeometries>
    {
        const string gtKey = "geometryType";
        const string gmKey = "geometries";

        public override GGeometries ReadJson(JsonReader reader, Type objectType, GGeometries existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JObject jObj = JObject.Load(reader);

            GGeometries result = null;
            if (jObj.ContainsKey(gtKey))
            {
                result = new GGeometries
                {
                    geometryType = (string)jObj[gtKey]
                };
                if (jObj.ContainsKey(gmKey))
                {
                    switch (result.geometryType)
                    {
                        case GeometryHelper.POINT_GEOMETRY:
                            result.geometries = jObj[gmKey].ToObject<List<Point>>().Cast<IRestGeometry>().ToList();
                            break;
                        case GeometryHelper.POLYLINE_GEOMETRY:
                            result.geometries = jObj[gmKey].ToObject<List<Polyline>>().Cast<IRestGeometry>().ToList();
                            break;
                        case GeometryHelper.POLYGON_GEOMETRY:
                            result.geometries = jObj[gmKey].ToObject<List<Polygon>>().Cast<IRestGeometry>().ToList();
                            break;
                        case GeometryHelper.MULTIPOINT_GEOMETRY:
                            result.geometries = jObj[gmKey].ToObject<List<MultiPoint>>().Cast<IRestGeometry>().ToList();
                            break;
                        case GeometryHelper.ENVELOPE_GEOMETRY:
                            result.geometries = jObj[gmKey].ToObject<List<Envelope>>().Cast<IRestGeometry>().ToList();
                            break;
                        default:
                            break;
                    }
                }
            }
            return result;
        }

        public override void WriteJson(JsonWriter writer, GGeometries value, JsonSerializer serializer)
        {
            if (value == null)
                return;
            JObject jObj = new JObject();
            if (!String.IsNullOrWhiteSpace(value.geometryType))
            {
                jObj.Add(gtKey, value.geometryType);
                if (value.geometries != null)
                {
                    switch (value.geometryType)
                    {
                        case GeometryHelper.POINT_GEOMETRY:
                            jObj.Add(gmKey, JToken.FromObject(value.geometries.Cast<Point>().ToList()));
                            break;
                        case GeometryHelper.POLYLINE_GEOMETRY:
                            jObj.Add(gmKey, JToken.FromObject(value.geometries.Cast<Polyline>().ToList()));
                            break;
                        case GeometryHelper.POLYGON_GEOMETRY:
                            jObj.Add(gmKey, JToken.FromObject(value.geometries.Cast<Polygon>().ToList()));
                            break;
                        case GeometryHelper.MULTIPOINT_GEOMETRY:
                            jObj.Add(gmKey, JToken.FromObject(value.geometries.Cast<MultiPoint>().ToList()));
                            break;
                        case GeometryHelper.ENVELOPE_GEOMETRY:
                            jObj.Add(gmKey, JToken.FromObject(value.geometries.Cast<Envelope>().ToList()));
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
