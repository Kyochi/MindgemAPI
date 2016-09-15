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
        public void getPrivateDataTest()
        {
            String key = "***REMOVED***";
            String skey = "***REMOVED***";

            String balanceJson = kPrivateUserMod.getPrivateData("Balance", key, skey);
            Console.WriteLine(balanceJson);
            Assert.Fail();
        }

        [TestMethod]
        public void getHeaderTest()
        {
            MindgemAPI.utils.Encoder encoder = new MindgemAPI.utils.Encoder();
            String key = "5GFD+SGSf1dsf25sdgGDFG52-FD5122dfg21";
            Byte[] sign = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 };

            Dictionary<String, String> headerDictExpected = new Dictionary<string, string>();
            headerDictExpected.Add("API-Key: ", key);
            headerDictExpected.Add("API-Sign: ", Convert.ToBase64String(sign));

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

            sign = null;
            Dictionary<String, String> headerDictNull = kPrivateUserMod.getHeader(key, sign);
            Assert.IsNull(headerDictNull);
        }
    }
}
