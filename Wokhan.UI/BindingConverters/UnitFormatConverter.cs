using System;
using System.Globalization;
using Wokhan.Core.Core;

#if __WPF__
using System.Windows.Data;
#else
using Windows.UI.Xaml.Data;
#endif

namespace Wokhan.UI.BindingConverters
{
    public sealed class UnitFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            try
            {
                return UnitFormatter.FormatBytes(System.Convert.ToDouble(value), parameter as string);
            }
            catch
            {
                return "#ERR";
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
