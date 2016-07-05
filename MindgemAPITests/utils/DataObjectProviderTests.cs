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
            string json = @"{
                            ""error"": [],
                            ""result"": {
                                ""XDAOXETH"": {
                                    ""altname"": ""DAOETH"",
                                    ""aclass_base"": ""currency"",
                                    ""base"": ""XDAO"",
                                    ""aclass_quote"": ""currency"",
                                    ""quote"": ""XETH"",
                                    ""lot"": ""unit"",
                                    ""pair_decimals"": 5
                                },
                                ""XDAOXXBT"": {
                                    ""altname"": ""DAOXBT"",
                                    ""aclass_base"": ""currency"",
                                    ""base"": ""XDAO"",
                                    ""aclass_quote"": ""currency"",
                                    ""quote"": ""XXBT"",
                                    ""lot"": ""unit"",
                                    ""fee_volume_currency"": ""ZUSD"",
                                    ""margin_call"": 80,
                                    ""margin_stop"": 40
                                }
                            }
                        }";

            string jsonSelect = JObject.Parse(json).ToString();
            KrakenPairItem kp = JsonConvert.DeserializeObject<KrakenPairItem>(jsonSelect);
            Assert.AreEqual("XDAOXXBT", kp.krakenPairs[1]);
        }
    }
}