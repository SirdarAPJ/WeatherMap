using System;
using System.Globalization;
using Xamarin.Forms;

namespace WeatherMap.Converters
{
    public class TitleCaseValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return string.Empty;

            TextInfo myTI = new CultureInfo("pt-BR", false).TextInfo;

            return myTI.ToTitleCase((string)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
