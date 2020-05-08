using System;
using System.Globalization;
#if __WPF__
using System.Windows;
using System.Windows.Data;
#else
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
#endif

namespace Wokhan.UI.BindingConverters
{
    //[ValueConversion(typeof(object), typeof(bool))]
    public class ObjectToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (value != null && !String.IsNullOrEmpty(value.ToString().Trim()));
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => Convert(value, targetType, parameter, String.Empty);
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => ConvertBack(value, targetType, parameter, String.Empty);

    }

}
