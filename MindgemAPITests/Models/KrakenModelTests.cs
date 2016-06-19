using MindgemAPI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MindgemAPI.utils;
using MindgemAPI.dataobjects;

namespace MindgemAPI.Models.Tests
{
    [TestClass]
    public class KrakenModelTests
    {
        private KrakenModel kTest = new KrakenModel();
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
            Assert.IsTrue(kTest.getCurrentKrakenPrice(ethereum, euro) > 0);
            Assert.AreNotEqual(kTest.getCurrentKrakenPrice(ethereum, euro), double.NaN);
            Assert.AreEqual(kTest.getCurrentKrakenPrice(ethereum, fake), double.NaN);
            Assert.AreEqual(kTest.getCurrentKrakenPrice(fake, fake), double.NaN);
            Assert.AreEqual(kTest.getCurrentKrakenPrice(fake, euro), double.NaN);
            Assert.AreEqual(kTest.getCurrentKrakenPrice(euro, ethereum), double.NaN);
            Assert.AreEqual(kTest.getCurrentKrakenPrice(null, euro), double.NaN);
            Assert.AreEqual(kTest.getCurrentKrakenPrice(null, null), double.NaN);
            Assert.AreEqual(kTest.getCurrentKrakenPrice(ethereum, null), double.NaN);
        }

        [TestMethod]
        public void getTradesLastdayTest()
        {
            Assert.IsTrue(kTest.getTradesLastDay(ethereum, euro, "today") > 0);
            Assert.AreNotEqual(kTest.getTradesLastDay(ethereum, euro, "today"), double.NaN);
            Assert.AreEqual(kTest.getTradesLastDay(ethereum, fake, "today"), double.NaN);
            Assert.AreEqual(kTest.getTradesLastDay(fake, fake, "today"), double.NaN);
            Assert.AreEqual(kTest.getTradesLastDay(fake, euro, "today"), double.NaN);
            Assert.AreEqual(kTest.getTradesLastDay(euro, ethereum, "today"), double.NaN);
            Assert.AreEqual(kTest.getTradesLastDay(null, euro, "today"), double.NaN);
            Assert.AreEqual(kTest.getTradesLastDay(null, null, "today"), double.NaN);
            Assert.AreEqual(kTest.getTradesLastDay(ethereum, null, "today"), double.NaN);
            Assert.IsTrue(kTest.getTradesLastDay(ethereum, euro, "last24hours") > 0);
            Assert.AreNotEqual(kTest.getTradesLastDay(ethereum, euro, "last24hours"), double.NaN);
            Assert.AreEqual(kTest.getTradesLastDay(ethereum, fake, "last24hours"), double.NaN);
            Assert.AreEqual(kTest.getTradesLastDay(fake, fake, "last24hours"), double.NaN);
            Assert.AreEqual(kTest.getTradesLastDay(fake, euro, "last24hours"), double.NaN);
            Assert.AreEqual(kTest.getTradesLastDay(euro, ethereum, "last24hours"), double.NaN);
            Assert.AreEqual(kTest.getTradesLastDay(null, euro, "last24hours"), double.NaN);
            Assert.AreEqual(kTest.getTradesLastDay(null, null, "last24hours"), double.NaN);
            Assert.AreEqual(kTest.getTradesLastDay(ethereum, null, "last24hours"), double.NaN);
        }

        [TestMethod]
        public void getOpeningPriceTest()
        {
            Assert.IsTrue(kTest.getOpeningPrice(ethereum, euro) > 0);
            Assert.AreNotEqual(kTest.getOpeningPrice(ethereum, euro), double.NaN);
            Assert.AreEqual(kTest.getOpeningPrice(ethereum, fake), double.NaN);
            Assert.AreEqual(kTest.getOpeningPrice(fake, fake), double.NaN);
            Assert.AreEqual(kTest.getOpeningPrice(fake, euro), double.NaN);
            Assert.AreEqual(kTest.getOpeningPrice(euro, ethereum), double.NaN);
            Assert.AreEqual(kTest.getOpeningPrice(null, euro), double.NaN);
            Assert.AreEqual(kTest.getOpeningPrice(null, null), double.NaN);
            Assert.AreEqual(kTest.getOpeningPrice(ethereum, null), double.NaN);
        }

        [TestMethod]
        public void getTimeTest()
        {
            string json = @"
            
                {""unixtime"":1466332475,""rfc1123"":""Sun, 19 Jun 16 10:34:35 + 0000""}
            ";
            
            ServerItem siTest = dopTest.deserializeJsonToObject<ServerItem>(json);
            Assert.AreEqual(1466332475, siTest.unixtime);
            Assert.AreEqual("Sun, 19 Jun 16 10:34:35 + 0000", siTest.rfc);
            Assert.AreNotEqual(string.Empty, kTest.getServerTime("unixtime"));
            Assert.AreNotEqual(string.Empty, kTest.getServerTime("rfc"));
            Assert.AreEqual(string.Empty, kTest.getServerTime(fake));
        }
    }
}