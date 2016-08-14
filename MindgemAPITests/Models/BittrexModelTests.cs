using Microsoft.VisualStudio.TestTools.UnitTesting;
using MindgemAPI.Models.bittrex;
using MindgemAPI.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindgemAPITests.Models
{
    [TestClass]
    public class BittrexModelTests
    {
        private BittrexPublicMarketModel pTest = new BittrexPublicMarketModel();
        DataObjectProvider dopTest = new DataObjectProvider();
        private const string ethereum = "ETH";
        private const string bitcoin = "BTC";
        private const string litecoin = "LTC";
        private const string siacoin = "SC";
        private const string euro = "EUR";
        private const string dollar = "USD";
        private const string fake = "fakeCurrency";

        [TestMethod]
        public void getBittrexJsonTest()
        {
            Assert.AreEqual(pTest.getJson(null, ethereum, euro), string.Empty);
            //Assert.AreEqual(pTest.getJson("ticker", bitcoin, fake), string.Empty);
            Assert.AreNotEqual(pTest.getJson("ticker", bitcoin, ethereum), string.Empty);
        }
    }
}
