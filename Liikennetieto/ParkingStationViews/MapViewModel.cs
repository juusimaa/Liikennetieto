using Microsoft.Maps.MapControl.WPF;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Utility.Wpf;

namespace Liikennetieto.ParkingStationViews
{
    internal sealed class MapViewModel : BindingBase
    {
        private Location _mapCenterCoordinate;

        public ObservableCollection<ParkingStationWithDetails> ParkingStationDetails { get; private set; }

        public MapViewModel(IEnumerable<ParkingStationWithDetails> stations)
        {
            ParkingStationDetails = new ObservableCollection<ParkingStationWithDetails>(stations);
            MapCenterCoordinate = stations.First().Location;
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
    }
}
