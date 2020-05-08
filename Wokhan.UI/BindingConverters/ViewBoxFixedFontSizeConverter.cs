using System;
using System.Globalization;
#if __WPF__
using System.Windows;
using System.Windows.Data;
using System.Windows.Controls;
#else
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Controls;
#endif

namespace Wokhan.UI.BindingConverters
{
    public sealed partial class ViewBoxFixedSizeConverter : DependencyObject, IValueConverter
    {
        public Viewbox vb
        {
            get => (Viewbox)GetValue(vbProperty);
            set => SetValue(vbProperty, value);
        }
        

        internal static readonly DependencyProperty vbProperty = DependencyProperty.Register(nameof(vb), typeof(Viewbox), typeof(ViewBoxFixedSizeConverter), null);

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            //Viewbox vb = (Viewbox)parameter;
            //if (!(value is double)) return null;
            int d = (int)value;
            return 100 / d * 9;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotSupportedException();
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => Convert(value, targetType, parameter, String.Empty);
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => ConvertBack(value, targetType, parameter, String.Empty);

    }

}
