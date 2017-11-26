using Liikennetieto.ParkingStationViews;

namespace Liikennetieto
{
    internal sealed class MainWindowModel : BindingBase
    {
        public ParkingStationViewModel ParkingViewModel { get; set; }

        public MainWindowModel()
        {
            ParkingViewModel = new ParkingStationViewModel();
        }
    }
}
