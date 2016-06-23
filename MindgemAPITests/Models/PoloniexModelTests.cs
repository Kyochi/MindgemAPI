﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        private const string litecoin = "LTC";
        private const string siacoin = "SC";
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
            Assert.AreNotEqual(pTest.getPercentChange(bitcoin, ethereum), double.NaN);
            Assert.AreEqual(pTest.getPercentChange(bitcoin, fake), double.NaN);
            Assert.AreEqual(pTest.getPercentChange(fake, fake), double.NaN);
            Assert.AreEqual(pTest.getPercentChange(fake, bitcoin), double.NaN);
            Assert.AreEqual(pTest.getPercentChange(ethereum, bitcoin), double.NaN);
            Assert.AreEqual(pTest.getPercentChange(null, bitcoin), double.NaN);
            Assert.AreEqual(pTest.getPercentChange(null, null), double.NaN);
            Assert.AreEqual(pTest.getPercentChange(bitcoin, null), double.NaN);
        }

        [TestMethod]
        public void getLastExchangeRate()
        {
            Assert.AreNotEqual(pTest.getLastExchangeRate(ethereum, litecoin), double.NaN);
            Assert.AreNotEqual(pTest.getLastExchangeRate(ethereum), double.NaN);
            Assert.AreEqual(pTest.getLastExchangeRate(bitcoin, ethereum), double.NaN);
            Assert.AreEqual(pTest.getLastExchangeRate(bitcoin, bitcoin), double.NaN);
            Assert.AreEqual(pTest.getLastExchangeRate(bitcoin), double.NaN);
            Assert.AreEqual(pTest.getLastExchangeRate(bitcoin, fake), double.NaN);
            Assert.AreEqual(pTest.getLastExchangeRate(fake, fake), double.NaN);
            Assert.AreEqual(pTest.getLastExchangeRate(fake, bitcoin), double.NaN);
            Assert.AreEqual(pTest.getLastExchangeRate(ethereum, bitcoin), double.NaN);
            Assert.AreEqual(pTest.getLastExchangeRate(null, bitcoin), double.NaN);
            Assert.AreEqual(pTest.getLastExchangeRate(null, null), double.NaN);
            Assert.AreEqual(pTest.getLastExchangeRate(bitcoin, null), double.NaN);
        }

        [TestMethod]
        public void getPriceCurrencyTest()
        {
            Assert.AreNotEqual(pTest.getPriceCurrency(ethereum, euro), double.NaN);
            Assert.AreNotEqual(pTest.getPriceCurrency(siacoin, euro), double.NaN);
            Assert.AreEqual(pTest.getPriceCurrency(bitcoin, ethereum), double.NaN);
            Assert.AreEqual(pTest.getPriceCurrency(euro, ethereum), double.NaN);
            Assert.AreEqual(pTest.getPriceCurrency(ethereum, fake), double.NaN);
            Assert.AreEqual(pTest.getPriceCurrency(fake, fake), double.NaN);
            Assert.AreEqual(pTest.getPriceCurrency(fake, ethereum), double.NaN);
            Assert.AreEqual(pTest.getPriceCurrency(ethereum, ethereum), double.NaN);
            Assert.AreEqual(pTest.getPriceCurrency(null, bitcoin), double.NaN);
            Assert.AreEqual(pTest.getLastExchangeRate(null, null), double.NaN);
            Assert.AreEqual(pTest.getPriceCurrency(bitcoin, null), double.NaN);
        }
    }
}
