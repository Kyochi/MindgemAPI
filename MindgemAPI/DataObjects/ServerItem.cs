using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MindgemAPI.DataObjects
{
    public class ServerItem
    {

        public class ResultServer
        {
            public int unixtime { get; set; }

            public String rfc { get; set; }
        }

        public class ServerObject
        {
            public List<object> error { get; set; }
            public ResultServer result { get; set; }
        }
    }
}