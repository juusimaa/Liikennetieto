using Microsoft.Maps.MapControl.WPF;
using OulunLiikenneData;
using System;
using System.Globalization;

namespace Liikennetieto.ParkingStationViews
{
    internal class ParkingStationWithDetails : BindingBase
    {
        private ParkingStation _station;
        private ParkingDetail _details;

        public ParkingDetail Details
        {
            get { return _details; }
            set
            {
                _details = value;
                FreeSpace = GetFreeSpacePercent(value);
                NotifyPropertyChanged(nameof(Details));
                NotifyPropertyChanged(nameof(FreeSpace));
            }
        }

        public int FreeSpace { get; set; }

        public ParkingStation Station
        {
            get { return _station; }
            set
            {
                _station = value;
                Location = GetCoordinate(value.Geom);
            }
        }

        public Location Location { get; set; }

        private Location GetCoordinate(string c)
        {
            try
            {
                var str = c.Substring(c.IndexOf('[') + 1, c.LastIndexOf(']') - c.IndexOf('[') - 1).Split(',');

                if (double.TryParse(str[0], NumberStyles.Any, CultureInfo.InvariantCulture, out double lat) &&
                    double.TryParse(str[1], NumberStyles.Any, CultureInfo.InvariantCulture, out double lon))
                {
                    return new Location(lon, lat);
                }

                return null;
            }
            catch (ArgumentOutOfRangeException)
            {
                return null;
            }
        }

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
