// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using JsonTesting;
//
//    var data = ParkingStations.FromJson(jsonString);
//
//    Generated with https://quicktype.io/
//
namespace Liikennetieto
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public partial class ParkingStations
    {
        [JsonProperty("parkingstation")]
        public List<ParkingStation> ParkingStationList { get; set; }
    }

    public partial class ParkingStation
    {
        [JsonProperty("geom")]
        public string Geom { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public partial class ParkingStations
    {
        public static ParkingStations FromJson(string json) => JsonConvert.DeserializeObject<ParkingStations>(json, ParkingStationConverter.Settings);
    }

    public static class ParkingStationSerialize
    {
        public static string ToJson(this ParkingStations self) => JsonConvert.SerializeObject(self, ParkingStationConverter.Settings);
    }

    public class ParkingStationConverter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
        };
    }
}