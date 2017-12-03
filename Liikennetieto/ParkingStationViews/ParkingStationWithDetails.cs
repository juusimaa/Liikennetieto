using Microsoft.Maps.MapControl.WPF;
using OulunLiikenneData;
using Liikennetieto.ExtensionMethods;

namespace Liikennetieto.ParkingStationViews
{
    internal class ParkingStationWithDetails
    {
        private ParkingStation _station;
        private ParkingDetail _details;

        public ParkingDetail Details
        {
            get { return _details; }
            set
            {
                _details = value;
                FreeSpacePercent = GetFreeSpacePercent(value);
            }
        }

        public int FreeSpacePercent { get; set; }

        public ParkingStation Station
        {
            get { return _station; }
            set
            {
                _station = value;
                Location = value.Geom.GetCoordinate();
            }
        }

        public Location Location { get; set; }

        private int GetFreeSpacePercent(ParkingDetail d)
        {
            if (double.TryParse(d.Freespace, out double free) &&
                double.TryParse(d.Totalspace, out double total))
            {
                return (int)(100 - free / total * 100);
            }

            return 0;
        }
    }
}
