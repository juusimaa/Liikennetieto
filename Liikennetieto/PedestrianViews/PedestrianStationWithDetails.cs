using Liikennetieto.ExtensionMethods;
using Microsoft.Maps.MapControl.WPF;
using OulunLiikenneData;

namespace Liikennetieto.PedestrianViews
{
    internal class PedestrianStationWithDetails
    {
        private EcoStation _station;

        public Location Location { get; set; }

        public EcoStation Station
        {
            get { return _station; }
            set
            {
                _station = value;
                Location = value.Geom.GetCoordinate();
            }
        }

        public EcoStationDetail Details { get; set; }
    }
}
