using Microsoft.WindowsAzure.Mobile.Service;
using MindgemAPI.Models;
using MindgemAPI.Models.poloniex;
using System;
using System.Web.Http;

namespace MindgemAPI.Controllers
{
    [RoutePrefix("api")]
    public class MindgemController : ApiController
    {
        public ApiServices Services { get; set; }
        
        public const uint MAXUSERS = 10;

        KrakenPublicMarketModel[] dataAccount;
        public KrakenPublicMarketModel kModel;
        public PoloniexPublicMarketModel pModel;

        // Constructeur
        public MindgemController()
        {
            kModel = new KrakenPublicMarketModel();
            pModel = new PoloniexPublicMarketModel();
            dataAccount = new KrakenPublicMarketModel[MAXUSERS];
        }

        [Route("kraken/price/{from}/{to}")]
        public String getKrakenPrice(String from, String to)
        {
            return Convert.ToString(this.kModel.getCurrentTickerInfos("askInfo", "price", from, to));
        }

        [Route("kraken/volume/{type}/{from}/{to}")]
        public String getKrakenVolume(String from, String to, String type)
        {
            return Convert.ToString(this.kModel.getCurrentTickerInfos("volume", type, from, to));
        }

        [Route("kraken/bidinfo/{type}/{from}/{to}")]
        public String getBidInfo(String from, String to, String type)
        {
            return Convert.ToString(this.kModel.getCurrentTickerInfos("bidInfo", type, from, to));
        }

        [Route("kraken/lowest/{type}/{from}/{to}")]
        public String getKrakenLowestPrice(String from, String to, String type)
        {
            return Convert.ToString(this.kModel.getCurrentTickerInfos("lowPrice", type, from, to));
        }

        [Route("kraken/highest/{type}/{from}/{to}")]
        public String getKrakenHighestPrice(String from, String to, String type)
        {
            return Convert.ToString(this.kModel.getCurrentTickerInfos("highPrice", type, from, to));
        }

        [Route("kraken/servertime/{type}")]
        public String getServerTimeTimestamp(String type)
        {
            return kModel.getServerTime(type);
        }

        [Route("kraken/trades/{type}/{from}/{to}")]
        public String getKrakenTrades(String from, String to, String type)
        {
            return Convert.ToString(this.kModel.getCurrentTickerInfos("numberOfTrades", type, from, to));
        }

        [Route("kraken/price/opening/{from}/{to}")]
        public String getOpeningPrice(String from, String to)
        {
            return Convert.ToString(this.kModel.getCurrentTickerInfos("opening", String.Empty, from, to));
        }

        [Route("kraken/assetpairs")]
        public String getAssetPairs()
        {
            return Convert.ToString(this.kModel.getKrakenPairs());
        }

        /*-------------------------------------------*/

        [Route("poloniex/price/{from}/{to}")]
        public String getPoloniexPrice(String from, String to)
        {
            return Convert.ToString(this.pModel.getCurrentTickerInfos("last", from, to));
        }

        [Route("poloniex/currency/{name}/{type}")]
        public String getPoloniexCurrencies(String name, String type)
        {
            return Convert.ToString(this.pModel.getCurrencyDetails(name, type));
        }
    }
}
