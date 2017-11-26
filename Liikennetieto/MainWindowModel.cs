using Liikennetieto.MapViews;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Runtime.Caching;

namespace Liikennetieto
{
    /* TODO:
     *  - async download
     * */
    internal sealed class MainWindowModel : BindingBase
    {
        private const string detailsQuery = "https://www.oulunliikenne.fi/public_traffic_api/parking/parking_details.php?parkingid=";
        private TimeSpan _cacheTimeout = TimeSpan.FromHours(1.0);

        private ParkingStation _selectedStation;
        private string _stationDetail;
        private ObjectCache _cache;
        private DateTime _startTime;
        private CacheItemPolicy _policy;

        public ObservableCollection<ParkingStation> Stations { get; set; }

        public string StationDetail
        {
            get { return _stationDetail; }
            set
            {
                _stationDetail = value;
                NotifyPropertyChanged(nameof(StationDetail));
            }
        }

        public ParkingStation SelectedStation {
            get { return _selectedStation; }
            set
            {
                _selectedStation = value;
                NotifyPropertyChanged(nameof(SelectedStation));
                DownloadDetail(Convert.ToInt32(value.Id));
                MapViewModel.CurrentParkingStation = value;
            }
        }

        public MainWindowModel()
        {
            _policy = new CacheItemPolicy();
            _cache = MemoryCache.Default;
            _startTime = DateTime.Now;

            Stations = new ObservableCollection<ParkingStation>();
            MapViewModel = new MapViewModel();
            DownloadStations();
            SelectedStation = Stations.FirstOrDefault(s => s.Name.Contains("Kivisydän"));
        }

        public MapViewModel MapViewModel { get; set; }

        private void DownloadStations()
        {
            var client = new WebClient();
            var stationsData = client.DownloadString("https://www.oulunliikenne.fi/public_traffic_api/parking/parkingstations.php");
            var s = ParkingStations.FromJson(stationsData).ParkingStationList.OrderBy(n => n.Name);

            foreach (var station in s)
            {
                Stations.Add(station);
            }
        }

        private void DownloadDetail(int stationId)
        {
            if (DateTime.Now - _startTime > _cacheTimeout)
            {
                foreach (var element in MemoryCache.Default)
                {
                    MemoryCache.Default.Remove(element.Key);
                }
                _startTime = DateTime.Now;
            }

            var cachedDetails = _cache[$"detail{stationId}"] as ParkingDetail;

            if (cachedDetails == null)
            {
                var client = new WebClient();
                var detailsData = client.DownloadString(detailsQuery + stationId);
                var details = ParkingDetail.FromJson(detailsData);
                MapViewModel.CurrentParkingDetails = details;
                _cache.Set($"detail{stationId}", details, _policy);
            }
            else
            {
                MapViewModel.CurrentParkingDetails = cachedDetails;
            }
        }
    }
}
