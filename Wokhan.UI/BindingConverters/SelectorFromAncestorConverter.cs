using System;
using System.Globalization;
#if __WPF__
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
#else
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Controls.Primitives;
#endif

namespace Wokhan.UI.BindingConverters
{
    public sealed partial class SelectorFromAncestorConverter : DependencyObject, IValueConverter
    {
        //private FrameworkElement originalSource = null;
        public static readonly DependencyProperty ContextParameterProperty = DependencyProperty.Register(nameof(ContextParameter), typeof(FrameworkElement), typeof(SelectorFromAncestorConverter), new PropertyMetadata(null));
        public FrameworkElement ContextParameter
        {
            get => (FrameworkElement)GetValue(ContextParameterProperty);
            set => SetValue(ContextParameterProperty, value);
        }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (ContextParameter == null)
            {
                return false;
            }
            //originalSource = (FrameworkElement)value;
            var x = VisualTreeHelper.GetParent(ContextParameter);
            bool ret = false;
#if __WPF__
            throw new NotImplementedException(x.GetType() + " is not implemented yet in Wokhan WPF library");
#else
            ret = ((SelectorItem)x).IsSelected;
#endif
            return ret;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (ContextParameter == null)
            {
                return false;
            }
            var x = VisualTreeHelper.GetParent(ContextParameter);
#if __WPF__
            throw new NotImplementedException(x.GetType() + " is not implemented yet in Wokhan WPF library");
#else
            ((SelectorItem)x).IsSelected = (bool)value;
#endif
            return value;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => Convert(value, targetType, parameter, String.Empty);
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => ConvertBack(value, targetType, parameter, String.Empty);

    }
}
