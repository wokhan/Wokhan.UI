using System;
using System.Globalization;
#if __WPF__
using System.Windows.Data;
using Wokhan.UI.WPF.Extensions;
#else
using Wokhan.UWP.Extensions;
using Windows.UI.Xaml.Data;
#endif

namespace Wokhan.Shared.UI.Converters
{
    public sealed class TranslateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
            {
                throw new ArgumentException(nameof(value));
            }

            if (parameter is string prefix)
            {
                return $"{prefix}{value}".Translate();
            }
            else
            {
                return value.ToString().Translate();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => Convert(value, targetType, parameter, String.Empty);
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => ConvertBack(value, targetType, parameter, String.Empty);

    }
}
