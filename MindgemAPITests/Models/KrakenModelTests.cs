using MindgemAPI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MindgemAPI.Models.Tests
{
    [TestClass]
    public class KrakenModelTests
    {
        private KrakenModel kTest = new KrakenModel();

        // Je la mets quand même, même si elle va être déplacée dans une autre classe
        [TestMethod]
        public void getPairCodeTest()
        {
            Assert.AreEqual("XETHZEUR", kTest.getPairCode("kraken", "ETH", "EUR"));
            Assert.AreEqual("XETHZBTC", kTest.getPairCode("kraken", "ETH", "BTC"));
            Assert.AreEqual("XNAWZAK", kTest.getPairCode("kraken", "NAW", "AK"));
            Assert.AreNotEqual("XETHZEUR", kTest.getPairCode("AnyService", "ETH", "EUR"));
            Assert.AreEqual("API service not found", kTest.getPairCode("AnyService", "ETH", "EUR"));
        }

        [TestMethod]
        public void getJsonTest()
        {
            
        }
    }
}