using Newtonsoft.Json;
using System;

namespace MindgemAPI.utils
{
    public class DataObjectProvider
    {

        public T deserializeJsonToObject<T>(String json)
        {
            T dataObject = JsonConvert.DeserializeObject<T>(json);
            Console.WriteLine(dataObject.ToString());
            if (dataObject == null) throw new JsonException("Json deserialize fail");

            return dataObject;
        }
    }
}