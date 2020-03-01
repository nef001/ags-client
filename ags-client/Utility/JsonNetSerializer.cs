
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serialization;

namespace ags_client.Utility
{
    public class JsonNetSerializer : IRestSerializer
    {
        private readonly JsonSerializerSettings jss = new JsonSerializerSettings();
        public string Serialize(object obj) =>
            JsonConvert.SerializeObject(obj, jss);

        public string Serialize(Parameter parameter) =>
            JsonConvert.SerializeObject(parameter.Value, jss);

        public T Deserialize<T>(IRestResponse response) =>
            JsonConvert.DeserializeObject<T>(response.Content, jss);

        public string[] SupportedContentTypes { get; } =
        {
            "application/json", "text/json", "text/x-json", "text/javascript", "*+json", "text/plain"
        };

        public string ContentType { get; set; } = "application/json";

        public DataFormat DataFormat { get; } = DataFormat.Json;
    }
}
