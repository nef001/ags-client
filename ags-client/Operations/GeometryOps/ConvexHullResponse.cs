﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ags_client.Types.Geometry;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Bson;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;


namespace ags_client.Operations.GeometryOps
{
    [JsonConverter(typeof(ConvexHullResponseConverter))]
    public class ConvexHullResponse : BaseResponse
    {
        public string geometryType { get; set; }
        public IRestGeometry geometry { get; set; }

        /* The convex hull is typically a polygon but can also be a polyline or point in degenerate cases.
          */
    }

    public class ConvexHullResponseConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(ConvexHullResponse));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jo = JObject.Load(reader);
            ConvexHullResponse result = new ConvexHullResponse();
            result.geometryType = (string)jo["geometryType"];
            switch (result.geometryType)
            {
                case "esriGeometryPoint":
                    result.geometry = jo["geometry"].ToObject<Point>();
                    break;
                case "esriGeometryPolyline":
                    result.geometry = jo["geometry"].ToObject<Polyline>();
                    break;
                case "esriGeometryPolygon":
                    result.geometry = jo["geometry"].ToObject<Polygon>();
                    break;
                default:
                    break;
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
