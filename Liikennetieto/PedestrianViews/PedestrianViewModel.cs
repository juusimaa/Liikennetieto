using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Runtime.Caching;
using OulunLiikenneData;

namespace Liikennetieto.PedestrianViews
{
    internal sealed class PedestrianViewModel : StationBase
    {
        private const string _stationsQuery = "http://www.oulunliikenne.fi/public_traffic_api/eco_traffic/eco_counters.php";
        private const string _detailsQuery = "http://www.oulunliikenne.fi/public_traffic_api/eco_traffic/eco_counter_daydata.php?measurementPointId={0}&daysFromHistory={1}";

        public PedestrianViewModel()
        {
            Stations = new ObservableCollection<PedestrianStationWithDetails>();
            DownloadStations();
            MapViewModel = new MapViewModel(Stations.ToList());
        }

        public ObservableCollection<PedestrianStationWithDetails> Stations { get; set; }

        public MapViewModel MapViewModel { get; set; }

        private void DownloadStations()
        {
            var client = new WebClient();
            var stationsData = client.DownloadString(_stationsQuery);
            var s = EcoCounterStations.FromJson(stationsData).EcoStations.OrderBy(n => n.Name);

            foreach (var station in s)
            {
                var details = DownloadDetail(Convert.ToInt32(station.Id));
                Stations.Add(new PedestrianStationWithDetails { Details = details, Station = station });
            }
        }

        private EcoStationDetail DownloadDetail(int id)
        {
            if (DateTime.Now - startTime > cacheTimeout)
            {
                foreach (var element in MemoryCache.Default)
                {
                    MemoryCache.Default.Remove(element.Key);
                }
                startTime = DateTime.Now;
            }

            if (cache[$"detail{id}"] is EcoStationDetail cachedDetails)
            {
                return cachedDetails;
            }

            var client = new WebClient();
            var detailsData = client.DownloadString(string.Format(_detailsQuery, id, 7));
            var details = EcoStationDetail.FromJson(detailsData);
            cache.Set($"detail{id}", details, policy);
            return details;
        }        
    }
}
