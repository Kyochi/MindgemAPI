using Newtonsoft.Json;
using System;

namespace MindgemAPI.utils
{
    public class DataObjectProvider
    {

        public T deserializeJsonToObject<T>(String json)
        {
            T dataObject = JsonConvert.DeserializeObject<T>(json);

            if (dataObject == null) throw new JsonException("deserializerJson impossible");

            return dataObject;
        }
    }
}