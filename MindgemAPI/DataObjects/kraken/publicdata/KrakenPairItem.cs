using MindgemAPI.converter;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MindgemAPI.dataobjects.kraken.publicdata
{
    public class KrakenPairItem
    {
        //Ici qu'il faudrait désérialisé tout le contenu du résult pour récupérer tous les objets
        [JsonConverter(typeof(JsonObjectsToListConverter))]
        public HashSet<String> krakenPairs { get; set; }
    }
}