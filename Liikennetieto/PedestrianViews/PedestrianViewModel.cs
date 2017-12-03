using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Runtime.Caching;
using System.Threading.Tasks;
using OulunLiikenneData;

namespace Liikennetieto.PedestrianViews
{
    internal sealed class PedestrianViewModel : StationBase
    {
        private const string _stationsQuery = "http://www.oulunliikenne.fi/public_traffic_api/eco_traffic/eco_counters.php";
        private const string _detailsQuery = "http://www.oulunliikenne.fi/public_traffic_api/eco_traffic/eco_counter_daydata.php?measurementPointId={0}&daysFromHistory={1}";

        private MapViewModel _mapViewModel;
        private PedestrianStationWithDetails _selectedStation;

        public PedestrianViewModel()
        {
            Stations = new ObservableCollection<PedestrianStationWithDetails>();
            DownloadStations();            
        }

        public ObservableCollection<PedestrianStationWithDetails> Stations { get; set; }

        public PedestrianStationWithDetails SelectedStation
        {
            get { return _selectedStation; }
            set
            {
                _selectedStation = value;
                NotifyPropertyChanged(nameof(SelectedStation));
            }
        }

        public MapViewModel MapViewModel
        {
            get { return _mapViewModel; }
            set
            {
                _mapViewModel = value;
                NotifyPropertyChanged(nameof(MapViewModel));
            }
        }

        private void DownloadStations()
        {
            Task.Run(async () =>
            {
                var client = new WebClient();
                var data = await client.DownloadStringTaskAsync(_stationsQuery);
                var stations = EcoCounterStations.FromJson(data).EcoStations.OrderBy(n => n.Name);

                foreach (var station in stations)
                {
                    var details = await DownloadDetail(Convert.ToInt32(station.Id));
                    Stations.Add(new PedestrianStationWithDetails { Details = details, Station = station });
                }

                MapViewModel = new MapViewModel(Stations.ToList());
                MapViewModel.PushPinClicked += MapViewModel_PushPinClicked;
            });
        }

        private void MapViewModel_PushPinClicked(object sender, PedestrianStationWithDetails e)
        {
            SelectedStation = e;
        }

        private async Task<EcoStationDetail> DownloadDetail(int id)
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
            var data = await client.DownloadStringTaskAsync(string.Format(_detailsQuery, id, 7));

            var details = EcoStationDetail.FromJson(data);
            cache.Set($"detail{id}", details, policy);
            return details;
        }        
    }
}
