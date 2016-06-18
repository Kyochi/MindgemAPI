using MindgemAPI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MindgemAPI.Models.Tests
{
    [TestClass]
    public class KrakenModelTests
    {
        private KrakenModel kTest = new KrakenModel();
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
            Assert.IsTrue(kTest.getTradesLastDay(ethereum, euro) > 0);
            Assert.AreNotEqual(kTest.getTradesLastDay(ethereum, euro), double.NaN);
            Assert.AreEqual(kTest.getTradesLastDay(ethereum, fake), double.NaN);
            Assert.AreEqual(kTest.getTradesLastDay(fake, fake), double.NaN);
            Assert.AreEqual(kTest.getTradesLastDay(fake, euro), double.NaN);
            Assert.AreEqual(kTest.getTradesLastDay(euro, ethereum), double.NaN);
            Assert.AreEqual(kTest.getTradesLastDay(null, euro), double.NaN);
            Assert.AreEqual(kTest.getTradesLastDay(null, null), double.NaN);
            Assert.AreEqual(kTest.getTradesLastDay(ethereum, null), double.NaN);
        }

        [TestMethod]
        public void getTimeTest()
        {
            Assert.AreNotEqual("Indisponible", kTest.getServerTime());
        }
    }
}