using OulunLiikenneData;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Runtime.Caching;

namespace Liikennetieto.ParkingStationViews
{
    internal sealed class ParkingStationViewModel : BindingBase
    {
        private const string detailsQuery = "https://www.oulunliikenne.fi/public_traffic_api/parking/parking_details.php?parkingid=";
        private TimeSpan _cacheTimeout = TimeSpan.FromHours(1.0);

        private ObjectCache _cache;
        private DateTime _startTime;
        private CacheItemPolicy _policy;

        public ObservableCollection<ParkingStationWithDetails> Stations { get; set; }

        public ParkingStationViewModel()
        {
            _policy = new CacheItemPolicy();
            _cache = MemoryCache.Default;
            _startTime = DateTime.Now;

            Stations = new ObservableCollection<ParkingStationWithDetails>();

            DownloadStations();

            MapViewModel = new MapViewModel(Stations.ToList());
        }

        public MapViewModel MapViewModel { get; set; }

        private void DownloadStations()
        {
            var client = new WebClient();
            var stationsData = client.DownloadString("https://www.oulunliikenne.fi/public_traffic_api/parking/parkingstations.php");
            var s = ParkingStations.FromJson(stationsData).ParkingStationList.OrderBy(n => n.Name);

            foreach (var station in s)
            {
                var details = DownloadDetail(Convert.ToInt32(station.Id));
                Stations.Add(new ParkingStationWithDetails { Details = details, Station = station});
            }
        }

        private ParkingDetail DownloadDetail(int stationId)
        {
            if (DateTime.Now - _startTime > _cacheTimeout)
            {
                foreach (var element in MemoryCache.Default)
                {
                    MemoryCache.Default.Remove(element.Key);
                }
                _startTime = DateTime.Now;
            }
            
            if (_cache[$"detail{stationId}"] is ParkingDetail cachedDetails)
            {
                return cachedDetails;
            }

            var client = new WebClient();
            var detailsData = client.DownloadString(detailsQuery + stationId);
            var details = ParkingDetail.FromJson(detailsData);
            _cache.Set($"detail{stationId}", details, _policy);
            return details;
        }
    }
}
