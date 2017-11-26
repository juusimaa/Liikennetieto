using Microsoft.Maps.MapControl.WPF;
using System;
using System.Globalization;

namespace Liikennetieto.MapViews
{
    internal sealed class MapViewModel : BindingBase
    {
        private Location _mapCenterCoordinate;
        private ParkingStation _currentParkingStation;
        private ParkingDetail _currentParkingDetail;

        public MapViewModel()
        {
            MapCenterCoordinate = new Location();
        }

        public int FreeSpacePercent => GetFreeSpacePercent();

        public ParkingStation CurrentParkingStation
        {
            get { return _currentParkingStation; }
            set
            {
                _currentParkingStation = value;
                NotifyPropertyChanged(nameof(CurrentParkingStation));
                SetCoordinate(value.Geom);
                NotifyPropertyChanged(nameof(FreeSpacePercent));
            }
        }

        public ParkingDetail CurrentParkingDetails
        {
            get { return _currentParkingDetail; }
            set
            {
                _currentParkingDetail = value;
                NotifyPropertyChanged(nameof(CurrentParkingDetails));
            }
        }

        public Location MapCenterCoordinate
        {
            get { return _mapCenterCoordinate; }
            set
            {
                _mapCenterCoordinate = value;
                NotifyPropertyChanged(nameof(MapCenterCoordinate));
            }
        }

        private void SetCoordinate(string c)
        {
            try
            {
                var str = c.Substring(c.IndexOf('[') + 1, c.LastIndexOf(']') - c.IndexOf('[') - 1).Split(',');
                
                if (double.TryParse(str[0], NumberStyles.Any, CultureInfo.InvariantCulture, out double lat) && 
                    double.TryParse(str[1], NumberStyles.Any, CultureInfo.InvariantCulture, out double lon))
                {
                    MapCenterCoordinate = new Location(lon, lat);
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                return;
            }
        }

        private int GetFreeSpacePercent()
        {
            if (double.TryParse(CurrentParkingDetails.Freespace, out double free) && double.TryParse(CurrentParkingDetails.Totalspace, out double total))
            {
                return (int)(free / total * 100);
            }

            return 0;
        }
    }
}
