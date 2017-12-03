// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using OulunLiikenneData;
//
//    var data = EcoCounterStations.FromJson(jsonString);
//
namespace OulunLiikenneData
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    public partial class EcoCounterStations
    {
        [JsonProperty("ecostation")]
        public List<EcoStation> EcoStations { get; set; }
    }

    public partial class EcoStation
    {
        [JsonProperty("direction_name")]
        public string DirectionName { get; set; }

        [JsonProperty("geom")]
        public string Geom { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public partial class EcoCounterStations
    {
        public static EcoCounterStations FromJson(string json) => JsonConvert.DeserializeObject<EcoCounterStations>(json, EcoStationConverter.Settings);
    }

    public static class EcoStationSerialize
    {
        public static string ToJson(this EcoCounterStations self) => JsonConvert.SerializeObject(self, EcoStationConverter.Settings);
    }

    public class EcoStationConverter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
        };
    }
}
