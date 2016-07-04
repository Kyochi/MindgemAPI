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
            HashSet<String> listObjectName = new HashSet<string>();

            foreach (JToken objects2 in jtoken.Children())
            {
                foreach (JObject jo in objects2.Children())
                {
                    if (!jo.Path.Contains(".d"))
                    {
                        listObjectName.Add(jo.Path);
                    }
                }
            }

            return listObjectName;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}