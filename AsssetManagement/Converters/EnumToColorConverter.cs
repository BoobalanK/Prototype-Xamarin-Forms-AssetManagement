using AsssetManagement.Models;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

using Xamarin.Forms;

namespace AsssetManagement.Converters
{
    public class EnumToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((AssetStatus)Enum.Parse(typeof(AssetStatus), value.ToString()))
            {
                case AssetStatus.InStore:
                    return Color.Green;
                case AssetStatus.Requested:
                    return Color.Red;
                case AssetStatus.Assigned:
                    return Color.Yellow;
                default:
                    return Color.Green;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
