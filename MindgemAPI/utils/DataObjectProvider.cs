using Newtonsoft.Json;
using System;
using System.Diagnostics;

namespace MindgemAPI.utils
{
    public class DataObjectProvider
    {

        public T deserializeJsonToObject<T>(String json, JsonConverter converter = null)
        {
            T dataObject = default(T);
            if (converter != null)
            {
                dataObject = JsonConvert.DeserializeObject<T>(json, converter);
            }
            else
            {
                dataObject = JsonConvert.DeserializeObject<T>(json);
            }

            try
            {
                if (dataObject == null) throw new JsonException("Json deserialize fail");
            }
            catch (JsonException jsonEx)
            {
                Debug.WriteLine(jsonEx.Message);
                
            }

            return dataObject;
        }
    }
}