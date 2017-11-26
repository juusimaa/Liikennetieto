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
        public List<Ecostation> Ecostation { get; set; }
    }

    public partial class Ecostation
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
        public static EcoCounterStations FromJson(string json) => JsonConvert.DeserializeObject<EcoCounterStations>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this EcoCounterStations self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    public class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
        };
    }
}
