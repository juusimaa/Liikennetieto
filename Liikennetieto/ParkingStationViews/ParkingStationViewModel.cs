using OulunLiikenneData;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Runtime.Caching;

namespace Liikennetieto.ParkingStationViews
{
    internal sealed class ParkingStationViewModel : StationBase
    {
        private const string _detailsQuery = "https://www.oulunliikenne.fi/public_traffic_api/parking/parking_details.php?parkingid=";
        private const string _stationsQuery = "https://www.oulunliikenne.fi/public_traffic_api/parking/parkingstations.php";
        
        public ObservableCollection<ParkingStationWithDetails> Stations { get; set; }

        public ParkingStationViewModel()
        {
            Stations = new ObservableCollection<ParkingStationWithDetails>();

            DownloadStations();

            MapViewModel = new MapViewModel(Stations.ToList());
        }

        public MapViewModel MapViewModel { get; set; }

        private void DownloadStations()
        {
            var client = new WebClient();
            var stationsData = client.DownloadString(_stationsQuery);
            var s = ParkingStations.FromJson(stationsData).ParkingStationList.OrderBy(n => n.Name);

            foreach (var station in s)
            {
                var details = DownloadDetail(Convert.ToInt32(station.Id));
                Stations.Add(new ParkingStationWithDetails { Details = details, Station = station});
            }
        }

        private ParkingDetail DownloadDetail(int stationId)
        {
            if (DateTime.Now - startTime > cacheTimeout)
            {
                foreach (var element in MemoryCache.Default)
                {
                    MemoryCache.Default.Remove(element.Key);
                }
                startTime = DateTime.Now;
            }
            
            if (cache[$"detail{stationId}"] is ParkingDetail cachedDetails)
            {
                return cachedDetails;
            }

            var client = new WebClient();
            var detailsData = client.DownloadString(_detailsQuery + stationId);
            var details = ParkingDetail.FromJson(detailsData);
            cache.Set($"detail{stationId}", details, policy);
            return details;
        }
    }
}
