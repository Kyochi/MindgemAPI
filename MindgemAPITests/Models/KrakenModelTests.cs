using MindgemAPI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MindgemAPI.utils;
using MindgemAPI.dataobjects;
using System;

namespace MindgemAPI.Models.Tests
{
    [TestClass]
    public class KrakenModelTests
    {
        private KrakenPublicMarketModel kTest = new KrakenPublicMarketModel();
        DataObjectProvider dopTest = new DataObjectProvider();
        private const string ethereum = "ETH";
        private const string bitcoin = "BTC";
        private const string euro = "EUR";
        private const string dollar = "USD";
        private const string fake = "fakeCurrency";

        [TestMethod]
        public void getJsonTest()
        {
            Assert.AreEqual(kTest.getJson(null, ethereum, euro), string.Empty);
            Assert.AreEqual(kTest.getJson("ticker", ethereum, fake), string.Empty);
            Assert.AreNotEqual(kTest.getJson("ticker", ethereum, euro), string.Empty);
        }

        [TestMethod]
        public void getCurrentPriceTest()
        {
            Assert.IsTrue(kTest.getCurrentTickerInfos("askInfo", "price", ethereum, euro) > 0);
            Assert.AreNotEqual(kTest.getCurrentTickerInfos("askInfo", "price", ethereum, euro), double.NaN);
            Assert.AreEqual(kTest.getCurrentTickerInfos("askInfo", "price", ethereum, fake), double.NaN);
            Assert.AreEqual(kTest.getCurrentTickerInfos("askInfo", "price", fake, fake), double.NaN);
            Assert.AreEqual(kTest.getCurrentTickerInfos("askInfo", "price", fake, euro), double.NaN);
            Assert.AreEqual(kTest.getCurrentTickerInfos("askInfo", "price", euro, ethereum), double.NaN);
            Assert.AreEqual(kTest.getCurrentTickerInfos("askInfo", "price", null, euro), double.NaN);
            Assert.AreEqual(kTest.getCurrentTickerInfos("askInfo", "price", null, null), double.NaN);
            Assert.AreEqual(kTest.getCurrentTickerInfos("askInfo", "price", ethereum, null), double.NaN);
        }

        [TestMethod]
        public void getTradesLastdayTest()
        {
            Assert.IsTrue(kTest.getCurrentTickerInfos("numberOfTrades", "today", ethereum, euro) > 0);
            Assert.AreNotEqual(kTest.getCurrentTickerInfos("numberOfTrades", "today", ethereum, euro), double.NaN);
            Assert.AreEqual(kTest.getCurrentTickerInfos("numberOfTrades", "today", ethereum, fake), double.NaN);
            Assert.AreEqual(kTest.getCurrentTickerInfos("numberOfTrades", "today", fake, fake), double.NaN);
            Assert.AreEqual(kTest.getCurrentTickerInfos("numberOfTrades", "today", fake, euro), double.NaN);
            Assert.AreEqual(kTest.getCurrentTickerInfos("numberOfTrades", "today", euro, ethereum), double.NaN);
            Assert.AreEqual(kTest.getCurrentTickerInfos("numberOfTrades", "today", null, euro), double.NaN);
            Assert.AreEqual(kTest.getCurrentTickerInfos("numberOfTrades", "today", null, null), double.NaN);
            Assert.AreEqual(kTest.getCurrentTickerInfos("numberOfTrades", "today", ethereum, null), double.NaN);
            Assert.IsTrue(kTest.getCurrentTickerInfos("numberOfTrades", "last24hours", ethereum, euro) > 0);
            Assert.AreNotEqual(kTest.getCurrentTickerInfos("numberOfTrades", "last24hours", ethereum, euro), double.NaN);
            Assert.AreEqual(kTest.getCurrentTickerInfos("numberOfTrades", "last24hours", ethereum, fake), double.NaN);
            Assert.AreEqual(kTest.getCurrentTickerInfos("numberOfTrades", "last24hours", fake, fake), double.NaN);
            Assert.AreEqual(kTest.getCurrentTickerInfos("numberOfTrades", "last24hours", fake, euro), double.NaN);
            Assert.AreEqual(kTest.getCurrentTickerInfos("numberOfTrades", "last24hours", euro, ethereum), double.NaN);
            Assert.AreEqual(kTest.getCurrentTickerInfos("numberOfTrades", "last24hours", null, euro), double.NaN);
            Assert.AreEqual(kTest.getCurrentTickerInfos("numberOfTrades", "last24hours", null, null), double.NaN);
            Assert.AreEqual(kTest.getCurrentTickerInfos("numberOfTrades", "last24hours", ethereum, null), double.NaN);
        }

        [TestMethod]
        public void getOpeningPriceTest()
        {
            Assert.IsTrue(kTest.getCurrentTickerInfos("opening", String.Empty, ethereum, euro) > 0);
            Assert.AreNotEqual(kTest.getCurrentTickerInfos("opening", String.Empty, ethereum, euro), double.NaN);
            Assert.AreEqual(kTest.getCurrentTickerInfos("opening", String.Empty, ethereum, fake), double.NaN);
            Assert.AreEqual(kTest.getCurrentTickerInfos("opening", String.Empty, fake, fake), double.NaN);
            Assert.AreEqual(kTest.getCurrentTickerInfos("opening", String.Empty, fake, euro), double.NaN);
            Assert.AreEqual(kTest.getCurrentTickerInfos("opening", String.Empty, euro, ethereum), double.NaN);
            Assert.AreEqual(kTest.getCurrentTickerInfos("opening", String.Empty, null, euro), double.NaN);
            Assert.AreEqual(kTest.getCurrentTickerInfos("opening", String.Empty, null, null), double.NaN);
            Assert.AreEqual(kTest.getCurrentTickerInfos("opening", String.Empty, ethereum, null), double.NaN);
        }

        [TestMethod]
        public void getTimeTest()
        {
            string json = @"
            
                {""unixtime"":1466332475,""rfc1123"":""Sun, 19 Jun 16 10:34:35 + 0000""}
            ";

            KrakenServerItem siTest = dopTest.deserializeJsonToObject<KrakenServerItem>(json);
            Assert.AreEqual(1466332475, siTest.unixtime);
            Assert.AreEqual("Sun, 19 Jun 16 10:34:35 + 0000", siTest.rfc);
            Assert.AreNotEqual(string.Empty, kTest.getServerTime("unixtime"));
            Assert.AreNotEqual(string.Empty, kTest.getServerTime("rfc"));
            Assert.AreEqual(string.Empty, kTest.getServerTime(fake));
        }

        [TestMethod]
        public void threadLoadPublicTickerKrakenTest()
        {
            
        }
    }
}