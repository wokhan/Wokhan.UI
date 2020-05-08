using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
#if __WPF__
using System.Windows;
using System.Windows.Data;
#else
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
#endif

namespace Wokhan.UI.BindingConverters
{
    public sealed partial class DictionaryMapper : DependencyObject, IValueConverter
    {
        public static readonly DependencyProperty CustomParameterProperty = DependencyProperty.Register(nameof(CustomParameter), typeof(string), typeof(DictionaryMapper), new PropertyMetadata(null));
        public string CustomParameter
        {
            get => (string)GetValue(CustomParameterProperty);
            set => SetValue(CustomParameterProperty, value);
        }

        private IDictionary<string, IList<string>> _previous;
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            IList<string> ret = null;
            if (CustomParameter != null)
            {
                var v = value as IDictionary<string, IList<string>>;
                _previous = v;
                v?.TryGetValue(CustomParameter, out ret);
            }
            return ret;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {

            if (CustomParameter != null && _previous != null)
            {
                var v = value as IList<object>;
                _previous[CustomParameter] = v?.Select(x => x.ToString()).ToList();
                //return new Dictionary<string, IList<string>> { { CustomParameter, v.Select(x => x.ToString()).ToList() } };
            }

            return _previous;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => Convert(value, targetType, parameter, String.Empty);
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => ConvertBack(value, targetType, parameter, String.Empty);

    }
}
