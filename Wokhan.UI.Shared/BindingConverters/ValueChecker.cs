using System;
using System.Globalization;
#if __WPF__
using System.Windows.Data;
#else
using Windows.UI.Xaml.Data;
#endif

namespace Wokhan.Shared.UI.Converters
{
    public sealed class ValueChecker : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return parameter == null || (value?.ToString().Equals(parameter) ?? false);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => Convert(value, targetType, parameter, String.Empty);
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => ConvertBack(value, targetType, parameter, String.Empty);

    }
}
