// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using JsonTesting;
//
//    var data = ParkingDetail.FromJson(jsonString);
//
//    Generated with https://quicktype.io/
//
namespace OulunLiikenneData
{
    using Newtonsoft.Json;

    public partial class ParkingDetail
    {
        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("freespace")]
        public string Freespace { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }

        [JsonProperty("totalspace")]
        public string Totalspace { get; set; }
    }

    public partial class ParkingDetail
    {
        public static ParkingDetail FromJson(string json) => JsonConvert.DeserializeObject<ParkingDetail>(json, ParkingDetailConverter.Settings);
    }

    public static class ParkingDetailSerialize
    {
        public static string ToJson(this ParkingDetail self) => JsonConvert.SerializeObject(self, ParkingDetailConverter.Settings);
    }

    public class ParkingDetailConverter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
        };
    }
}

