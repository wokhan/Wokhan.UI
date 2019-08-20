using System;
using System.Globalization;
#if __WPF__
using System.Windows;
using System.Windows.Data;
#else
using Windows.UI.Xaml.Data;
#endif

namespace Wokhan.Shared.UI.Converters
{
    public sealed class BooleanNegateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return !(bool)value;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => Convert(value, targetType, parameter, String.Empty);
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => ConvertBack(value, targetType, parameter, String.Empty);

    }
}
