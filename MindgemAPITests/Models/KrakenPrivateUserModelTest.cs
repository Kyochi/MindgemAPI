using Microsoft.VisualStudio.TestTools.UnitTesting;
using MindgemAPI.Models.kraken;
using MindgemAPI.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindgemAPITests.Models
{
    [TestClass]
    public class KrakenPrivateUserModelTest
    {
        public KrakenPrivateUserModel kPrivateUserMod = new KrakenPrivateUserModel();

        [TestMethod]
        public void getHeaderTest()
        {
            MindgemAPI.utils.Encoder encoder = new MindgemAPI.utils.Encoder();
            String key = "5GFD+SGSf1dsf25sdgGDFG52-FD5122dfg21";
            String sign = "25ca8755fc3abd6756915e3969e4595864dc1beac5ae3c3116093979a6574c135088fd686b7b5d2c341f25a1040e0b982f8f53ceeda5978e2907c6cbd63d132c";

            Dictionary<String, String> headerDictExpected = new Dictionary<string, string>();
            headerDictExpected.Add("API-Key: ", key);
            headerDictExpected.Add("API-Sign: ", encoder.Base64Encode(sign));

            Dictionary<String, String> headerDict = kPrivateUserMod.getHeader(key, sign);
            Boolean isOk = true;
            foreach (var elExpected in headerDictExpected)
            {
                if (headerDict[elExpected.Key] != elExpected.Value )
                {
                    isOk = false;
                }
            }
            Assert.IsTrue(isOk);
            
        }
    }
}
