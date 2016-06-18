using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MindgemAPI.dataobjects
{
    public class ServerItem
    {
        [JsonProperty("unixtime")]
        public int unixtime { get; set; }

        [JsonProperty("rfc1123")]
        public String rfc { get; set; }
    }
}