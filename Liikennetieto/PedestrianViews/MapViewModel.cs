using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Microsoft.Maps.MapControl.WPF;
using Utility.Wpf;

namespace Liikennetieto.PedestrianViews
{
    internal class MapViewModel : BindingBase
    {
        private Location _mapCenterCoordinate;
        private Visibility _waitLabelVisibility;

        public event EventHandler<PedestrianStationWithDetails> PushPinClicked;

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

        public ICommand PushPinCommand => new RelayCommand(OnPusnPinClicked);

        public void OnPusnPinClicked(object d)
        {
            PushPinClicked?.Invoke(this, d as PedestrianStationWithDetails);
        }
    }
}
