using System;
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


namespace ags_client.Operations.GeometryOps
{
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
            Node node = new Node();
            node.vr = (string)jo["vr"];
            if (node.vr == "PN")
            {
                node.Value = jo["Value"].ToObject<List<PnItem>>(serializer);
            }
            else if (node.vr == "SQ")
            {
                node.Value = jo["Value"].ToObject<List<Dictionary<string, Node>>>(serializer);
            }
            else
            {
                node.Value = jo["Value"].ToObject<List<string>>(serializer);
            }
            return node;
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
