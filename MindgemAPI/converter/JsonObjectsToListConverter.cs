using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MindgemAPI.converter
{
    public class JsonObjectsToListConverter : JsonConverter
    {

        public JsonObjectsToListConverter()
        {

        }

        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(HashSet<String>));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JToken jtoken = JToken.Load(reader);
            JObject jObjectCast = jtoken.Value<JObject>();

            List<String> listPairs = (from prop in jObjectCast.Properties()
                                    where !prop.Name.Contains(".d")
                                    select prop.Name).ToList();
            return listPairs;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}