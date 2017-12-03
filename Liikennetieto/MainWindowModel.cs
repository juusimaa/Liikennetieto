using Liikennetieto.ParkingStationViews;
using Liikennetieto.PedestrianViews;

namespace Liikennetieto
{
    internal sealed class MainWindowModel : BindingBase
    {
        public ParkingStationViewModel ParkingViewModel { get; set; }

        public PedestrianViews.PedestrianViewModel PedestrianViewModel { get; set; }

        public MainWindowModel()
        {
            ParkingViewModel = new ParkingStationViewModel();
            PedestrianViewModel = new PedestrianViewModel();
        }
    }
}
