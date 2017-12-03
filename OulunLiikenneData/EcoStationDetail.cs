// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using Oulunliikenne;
//
//    var data = EcoStationDetail.FromJson(jsonString);
//
//    Generated with https://quicktype.io/
//
namespace OulunLiikenneData
{
    using Newtonsoft.Json;

    public partial class EcoStationDetail
    {
        [JsonProperty("ecoCounterDayResults")]
        public EcoCounterDayResult[] EcoCounterDayResults { get; set; }

        [JsonProperty("resultTitle")]
        public string ResultTitle { get; set; }

        [JsonProperty("yearMaxDate")]
        public string YearMaxDate { get; set; }

        [JsonProperty("yearMaxValue")]
        public string YearMaxValue { get; set; }

        [JsonProperty("yearMaxWeekday")]
        public string YearMaxWeekday { get; set; }
    }

    public partial class EcoCounterDayResult
    {
        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("weekday")]
        public string Weekday { get; set; }
    }

    public partial class EcoStationDetail
    {
        public static EcoStationDetail FromJson(string json) => JsonConvert.DeserializeObject<EcoStationDetail>(json, EcoStationDetailConverter.Settings);
    }

    public static class EcoStationDetailSerialize
    {
        public static string ToJson(this EcoStationDetail self) => JsonConvert.SerializeObject(self, EcoStationDetailConverter.Settings);
    }

    public class EcoStationDetailConverter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
        };
    }
}

