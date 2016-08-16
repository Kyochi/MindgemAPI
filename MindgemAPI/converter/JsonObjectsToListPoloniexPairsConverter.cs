using MindgemAPI.dataobjects.poloniex.publicdata;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MindgemAPI.converter
{
    public class JsonObjectsToListPoloniexPairsConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(PoloniexPairItem));
        }
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            PoloniexPairItem value = new PoloniexPairItem();
            JToken jtoken = JToken.Load(reader);
            JObject jObjectCast = jtoken.Value<JObject>();

            List<String> listitem = (from prop in jObjectCast.Properties()
                                     select prop.Name).ToList();
            return new PoloniexPairItem { listPairs = listitem };
        }
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}