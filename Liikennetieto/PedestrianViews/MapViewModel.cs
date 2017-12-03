using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Microsoft.Maps.MapControl.WPF;

namespace Liikennetieto.PedestrianViews
{
    internal class MapViewModel : BindingBase
    {
        private Location _mapCenterCoordinate;
        private Visibility _waitLabelVisibility;

        public ObservableCollection<PedestrianStationWithDetails> StationDetails { get; private set; }

        public Visibility WaitLabelVisibility
        {
            get { return _waitLabelVisibility; }
            set
            {
                _waitLabelVisibility = value;
                NotifyPropertyChanged(nameof(WaitLabelVisibility));
            }
        }

        public MapViewModel(IEnumerable<PedestrianStationWithDetails> stations)
        {
            StationDetails = new ObservableCollection<PedestrianStationWithDetails>(stations);
            MapCenterCoordinate = stations.First().Location;
            WaitLabelVisibility = Visibility.Collapsed;
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
