using MindgemAPI.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MindgemAPI.Models.kraken
{
    public class KrakenPrivateUserModel
    {
        public Encoder encoder = new Encoder();
        public String getPrivateData(String operationType, String apiKey, String privateApiKey)
        {
            return "";
        }

        public Dictionary<String, String> getHeader(String apikey, String sign)
        {
            Dictionary<String, String> headers = null;
            if (apikey != null && sign != null)
            {
                headers = new Dictionary<String, String>();
                headers.Add("API-Key: ", apikey);
                headers.Add("API-Sign: ", encoder.Base64Encode(sign));
            }
            return headers;
        }
    }
}