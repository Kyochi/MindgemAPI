﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace MindgemAPI.converter
{
    public class JsonTickerConverter : JsonConverter
    {
        private string[] keysArray;

        public JsonTickerConverter(Type containingObjectType, string keysArrayFieldName)
        {
            FieldInfo field = containingObjectType.GetField(keysArrayFieldName);
            if (field == null)
                throw new Exception("Champs : " + keysArrayFieldName + " du type " + containingObjectType.Name + " introuvable.");
            if (!field.Attributes.HasFlag(FieldAttributes.Static) || field.FieldType != typeof(String[]))
                throw new Exception("Le champs " + keysArrayFieldName + " de " + containingObjectType.Name + " doit être déclarée statique");
            keysArray = (string[])field.GetValue(null);
        }

        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(Dictionary<string, object>));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JArray array = JArray.Load(reader);
            Dictionary<string, object> dict = new Dictionary<string, object>();

            for (int i = 0; i < array.Count; i++)
            {
                string key = i < keysArray.Length ? keysArray[i] : "key undefined " + i;
                dict.Add(key, (string)array[i]);
            }

            return dict;
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