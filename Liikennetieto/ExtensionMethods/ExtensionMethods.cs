using System;
using System.Globalization;
using Microsoft.Maps.MapControl.WPF;

namespace Liikennetieto.ExtensionMethods
{
    internal static class ExtensionMethods
    {
        internal static Location GetCoordinate(this string c)
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
    }
}
