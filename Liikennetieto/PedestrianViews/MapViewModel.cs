using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Maps.MapControl.WPF;

namespace Liikennetieto.PedestrianViews
{
    internal class MapViewModel : BindingBase
    {
        private Location _mapCenterCoordinate;

        public ObservableCollection<PedestrianStationWithDetails> StationDetails { get; private set; }

        public MapViewModel(IEnumerable<PedestrianStationWithDetails> stations)
        {
            StationDetails = new ObservableCollection<PedestrianStationWithDetails>(stations);
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
