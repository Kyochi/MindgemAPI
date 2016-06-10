using Microsoft.VisualStudio.TestTools.UnitTesting;
using MindgemAPI.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindgemAPI.utils.Tests
{
    [TestClass()]
    public class UrlBuilderTests
    {
        public UrlBuilder testUrlBuilder = new UrlBuilder();

        [TestMethod]
        public void getPairCodeTest()
        {
            Assert.AreEqual("XETHZEUR", testUrlBuilder.getPairCode("kraken", "ETH", "EUR"));
            Assert.AreEqual("XETHZBTC", testUrlBuilder.getPairCode("kraken", "ETH", "BTC"));
            Assert.AreEqual("XNAWZAK", testUrlBuilder.getPairCode("kraken", "NAW", "AK"));
            Assert.AreNotEqual("XETHZEUR", testUrlBuilder.getPairCode("AnyService", "ETH", "EUR"));
            Assert.AreEqual("API service not found", testUrlBuilder.getPairCode("AnyService", "ETH", "EUR"));
        }

        [TestMethod]
        [ExpectedException(typeof(exception.UrlBuilderException),"Le service est null")]
        public void getPairCodeTestRaiseException()
        {
            testUrlBuilder.getPairCode(null, "ETH", "EUR");
        }
    }
}