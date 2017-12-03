using Liikennetieto.ParkingStationViews;
using Liikennetieto.PedestrianViews;
using Utility.Wpf;

namespace Liikennetieto
{
    internal sealed class MainWindowModel : BindingBase
    {
        public ParkingStationViewModel ParkingViewModel { get; set; }

        public PedestrianViewModel PedestrianViewModel { get; set; }

        public MainWindowModel()
        {
            ParkingViewModel = new ParkingStationViewModel();
            PedestrianViewModel = new PedestrianViewModel();
        }
    }
}
