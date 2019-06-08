using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ags_client.Types;
using ags_client.Types.Geometry;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
                switch (value.geometryType)
                {
                    case GeometryHelper.POINT_GEOMETRY:
                        var point = value.geometry as Point;
                        jObj.Add(gmKey, JToken.FromObject(point));
                        break;
                    case GeometryHelper.POLYLINE_GEOMETRY:
                        var polyline = value.geometry as Polyline;
                        jObj.Add(gmKey, JToken.FromObject(polyline));
                        break;
                    case GeometryHelper.POLYGON_GEOMETRY:
                        var polygon = value.geometry as Polygon;
                        jObj.Add(gmKey, JToken.FromObject(polygon));
                        break;
                    case GeometryHelper.MULTIPOINT_GEOMETRY:
                        var multipoint = value.geometry as MultiPoint;
                        jObj.Add(gmKey, JToken.FromObject(multipoint));
                        break;
                    case GeometryHelper.ENVELOPE_GEOMETRY:
                        var envelope = value.geometry as Envelope;
                        jObj.Add(gmKey, JToken.FromObject(envelope));
                        break;
                    default:
                        break;
                }
            }
            writer.WriteValue(jObj.ToString());
        }
    }
}
