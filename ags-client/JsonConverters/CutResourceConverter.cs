﻿using ags_client.Resources.GeometryService;
using ags_client.Types;
using ags_client.Types.Geometry;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ags_client.JsonConverters
{
    public class CutResourceConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(CutResource));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jObj = JObject.Load(reader);

            var result = new CutResource();
            if (jObj.ContainsKey("error"))
            {
                result.error = jObj["error"].ToObject<ErrorDetail>();
            }
            if (jObj.ContainsKey("geometryType"))
            {
                result.geometryType = (string)jObj["geometryType"];
                if (jObj.ContainsKey("geometries"))
                {
                    result.geometries = new List<IRestGeometry>();
                    switch (result.geometryType)
                    {
                        case "esriGeometryPoint":
                            result.geometries.AddRange(jObj["geometries"].ToObject<List<Point>>().Select(x => (IRestGeometry)x));
                            break;
                        case "esriGeometryPolyline":
                            result.geometries.AddRange(jObj["geometries"].ToObject<List<Polyline>>().Select(x => (IRestGeometry)x));
                            break;
                        case "esriGeometryPolygon":
                            result.geometries.AddRange(jObj["geometries"].ToObject<List<Polygon>>().Select(x => (IRestGeometry)x));
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
