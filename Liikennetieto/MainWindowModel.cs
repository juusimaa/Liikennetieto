using Liikennetieto.MapViews;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;

namespace Liikennetieto
{
    /* TODO:
     *  - async download
     *  - cache data
     *  - get a life
     * 
     * */
    internal sealed class MainWindowModel : BindingBase
    {
        private const string detailsQuery = "https://www.oulunliikenne.fi/public_traffic_api/parking/parking_details.php?parkingid=";

        private ParkingStation _selectedStation;
        private string _stationDetail;

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
            var client = new WebClient();
            var detailsData = client.DownloadString(detailsQuery + stationId);
            var details = ParkingDetail.FromJson(detailsData);

            MapViewModel.CurrentParkingDetails = details;

            StationDetail = 
                $"Name: {details.Name}{Environment.NewLine}" +
                $"Address: {details.Address}{Environment.NewLine}" +
                $"Total space: {details.Totalspace}{Environment.NewLine}" +
                $"Available space: {details.Freespace}";
        }
    }
}
