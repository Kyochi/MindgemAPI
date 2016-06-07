using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MindgemAPI.DataObjects
{
    public class OrderBookItem
    {
        public class OrderBook
        {
            public List<List<string>> asks { get; set; }
            public List<List<string>> bids { get; set; }
        }

        public class ResultOrderBook
        {
            public OrderBook XETHZEUR { get; set; }
        }

        public class OrderBookObject
        {
            public List<object> error { get; set; }
            public ResultOrderBook result { get; set; }
        }
    }
}