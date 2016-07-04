using Microsoft.VisualStudio.TestTools.UnitTesting;
using MindgemAPI.dataobjects.kraken.publicdata;
using MindgemAPI.utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindgemAPI.utils.Tests
{
    [TestClass()]
    public class DataObjectProviderTests
    {
        [TestMethod]
        public void testDeserializePair()
        {
            string json = System.IO.File.ReadAllText(@"F:\DEV-PERSO\testjson.txt");
            string jsonSelect = JObject.Parse(json)["result"].ToString();
            KrakenPairItem kp = JsonConvert.DeserializeObject<KrakenPairItem>(jsonSelect);
            Assert.AreNotEqual(null, kp.krakenPairs);
        }
    }
}