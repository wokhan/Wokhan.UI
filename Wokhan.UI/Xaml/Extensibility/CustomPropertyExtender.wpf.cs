using System.Windows;
using System.Windows.Controls;

namespace Wokhan.UI.Xaml.Extensibility
{
    public class CustomPropertyExtender
    {
        public class CustomProperty
        {
            public string Title { get; set; }
            public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(nameof(Title), typeof(string), typeof(CustomProperty));
            
            public object Value { get; set; }
            public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(nameof(Value), typeof(object), typeof(CustomProperty));
        }

        public static readonly DependencyProperty CustomPropertiesProperty = DependencyProperty.RegisterAttached("CustomProperties", typeof(CustomProperty), typeof(ContentControl));
        public static void SetCustomProperties(Control control, CustomProperty value) => control.SetValue(CustomPropertiesProperty, value);
        public static CustomProperty GetCustomProperties(Control control) => (CustomProperty)control.GetValue(CustomPropertiesProperty);
    }
}
