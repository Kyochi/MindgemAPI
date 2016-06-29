using Microsoft.WindowsAzure.Mobile.Service;
using MindgemAPI.Models;
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

        // Constructeur
        public MindgemController()
        {
            kModel = new KrakenPublicMarketModel();
            dataAccount = new KrakenPublicMarketModel[MAXUSERS];
        }

        [Route("kraken/price/{from}/{to}")]
        public String getKrakenPrice(String from, String to)
        {
            return Convert.ToString(this.kModel.getCurrentTickerInfos("askInfo", "price", from, to));
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
            return Convert.ToString(this.kModel.refreshPairs());
        }
    }
}
