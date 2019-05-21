using System;
using System.Globalization;
using Xamarin.Forms;

namespace WeatherMap.Converters
{
    public class TemperatureValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return string.Format("{0:N0}° C", value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
