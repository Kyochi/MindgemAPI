﻿using System;
using System.Collections.Generic;

namespace MindgemAPI.dataobjects
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