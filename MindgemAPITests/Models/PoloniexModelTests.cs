using Microsoft.VisualStudio.TestTools.UnitTesting;
using MindgemAPI.Models.poloniex;
using MindgemAPI.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindgemAPITests.Models
{
    [TestClass]
    public class PoloniexModelTests
    {
        private PoloniexPublicMarketModel pTest = new PoloniexPublicMarketModel();
        DataObjectProvider dopTest = new DataObjectProvider();
        private const string ethereum = "ETH";
        private const string bitcoin = "BTC";
        private const string euro = "EUR";
        private const string dollar = "USD";
        private const string fake = "fakeCurrency";

        [TestMethod]
        public void getPoloniexJsonTest()
        {
            Assert.AreEqual(pTest.getJson(null, ethereum, euro), string.Empty);
            Assert.AreEqual(pTest.getJson("ticker", bitcoin, fake), string.Empty);
            Assert.AreNotEqual(pTest.getJson("ticker", bitcoin, ethereum), string.Empty);
        }

        [TestMethod]
        public void getPercentChangeTest()
        {
            Assert.IsTrue(pTest.getCurrentPrice(bitcoin, ethereum) > 0);
            Assert.AreNotEqual(pTest.getCurrentPrice(bitcoin, ethereum), double.NaN);
            Assert.AreEqual(pTest.getCurrentPrice(bitcoin, fake), double.NaN);
            Assert.AreEqual(pTest.getCurrentPrice(fake, fake), double.NaN);
            Assert.AreEqual(pTest.getCurrentPrice(fake, bitcoin), double.NaN);
            Assert.AreEqual(pTest.getCurrentPrice(ethereum, bitcoin), double.NaN);
            Assert.AreEqual(pTest.getCurrentPrice(null, bitcoin), double.NaN);
            Assert.AreEqual(pTest.getCurrentPrice(null, null), double.NaN);
            Assert.AreEqual(pTest.getCurrentPrice(bitcoin, null), double.NaN);
        }
    }
}
