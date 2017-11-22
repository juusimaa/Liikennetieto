using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;
using System.Runtime.CompilerServices;

namespace Liikennetieto
{
    /* TODO:
     *  - async download
     *  - cache data
     *  - get a life
     * 
     * */
    internal sealed class MainWindowModel : INotifyPropertyChanged
    {
        private const string detailsQuery = "https://www.oulunliikenne.fi/public_traffic_api/parking/parking_details.php?parkingid=";

        private Parkingstation _selectedStation;
        private string _stationDetail;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Parkingstation> Stations { get; set; }

        public string StationDetail
        {
            get { return _stationDetail; }
            set
            {
                _stationDetail = value;
                NotifyPropertyChanged(nameof(StationDetail));
            }
        }

        public Parkingstation SelectedStation {
            get { return _selectedStation; }
            set
            {
                _selectedStation = value;
                NotifyPropertyChanged(nameof(SelectedStation));
                DownloadDetail(Convert.ToInt32(value.Id));
            }
        }

        public MainWindowModel()
        {
            Stations = new ObservableCollection<Parkingstation>();
            DownloadStations();
        }

        private void DownloadStations()
        {
            var client = new WebClient();
            var stationsData = client.DownloadString("https://www.oulunliikenne.fi/public_traffic_api/parking/parkingstations.php");
            var s = ParkingStation.FromJson(stationsData).Parkingstations;

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

            StationDetail = 
                $"Name: {details.Name}{Environment.NewLine}" +
                $"Address: {details.Address}{Environment.NewLine}" +
                $"Total space: {details.Totalspace}{Environment.NewLine}" +
                $"Available space: {details.Freespace}";
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
