using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Effects;

namespace Wokhan.UI.Xaml.Extensibility
{
    public partial class CustomAdorner : Control
    {
        private static ControlTemplate template = (ControlTemplate)new CustomAdorner().Resources["Template"];

        private static Style DefaultImageStyle = new Style(typeof(ContentControl)) { Setters = { new Setter(MarginProperty, new Thickness(0, 0, 5, 0)) } };

        public static readonly DependencyProperty BackgroundEffectProperty = DependencyProperty.RegisterAttached("BackgroundEffect", typeof(Effect), typeof(Control), new PropertyMetadata(null, PropertyChanged));
        public static void SetBackgroundEffect(Control control, Effect value) => control.SetValue(BackgroundEffectProperty, value);
        public static Effect GetBackgroundEffect(Control control) => (Effect)control.GetValue(BackgroundEffectProperty);


        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.RegisterAttached("CornerRadius", typeof(CornerRadius), typeof(Control), new PropertyMetadata(new CornerRadius(), PropertyChanged));
        public static void SetCornerRadius(Control control, CornerRadius value) => control.SetValue(CornerRadiusProperty, value);
        public static CornerRadius GetCornerRadius(Control control) => (CornerRadius)control.GetValue(CornerRadiusProperty);


        public static readonly DependencyProperty ImageProperty = DependencyProperty.RegisterAttached("Image", typeof(object), typeof(Control), new PropertyMetadata(null, PropertyChanged));
        public static void SetImage(Control control, object value) => control.SetValue(ImageProperty, value);
        public static object GetImage(Control control) => control.GetValue(ImageProperty);


        public static readonly DependencyProperty ImageStyleProperty = DependencyProperty.RegisterAttached("ImageStyle", typeof(Style), typeof(Control), new PropertyMetadata(DefaultImageStyle, PropertyChanged));
        public static void SetImageStyle(Control control, Style value) => control.SetValue(ImageStyleProperty, value);
        public static Style GetImageStyle(Control control) => (Style)control.GetValue(ImageStyleProperty);


        public static readonly DependencyProperty OrientationProperty = DependencyProperty.RegisterAttached("Orientation", typeof(Orientation), typeof(Control), new PropertyMetadata(Orientation.Horizontal, PropertyChanged));
        public static void SetOrientation(Control control, Orientation value) => control.SetValue(OrientationProperty, value);
        public static Orientation GetOrientation(Control control) => (Orientation)control.GetValue(OrientationProperty);


        public static readonly DependencyProperty PreserveTemplateProperty = DependencyProperty.RegisterAttached("PreserveTemplate", typeof(bool), typeof(Control), new PropertyMetadata(false, PropertyChanged));
        public static void SetPreserveTemplate(Control control, bool value) => control.SetValue(PreserveTemplateProperty, value);
        public static bool GetPreserveTemplate(Control control) => (bool)control.GetValue(PreserveTemplateProperty);


        public static readonly DependencyProperty HeaderProperty = DependencyProperty.RegisterAttached("Header", typeof(object), typeof(Control), new PropertyMetadata(null, PropertyChanged));
        public static void SetHeader(Control control, object value) => control.SetValue(HeaderProperty, value);
        public static object GetHeader(Control control) => control.GetValue(HeaderProperty);


        public static readonly DependencyProperty HeaderImageProperty = DependencyProperty.RegisterAttached("HeaderImage", typeof(object), typeof(Control), new PropertyMetadata(null, PropertyChanged));
        public static void SetHeaderImage(Control control, object value) => control.SetValue(HeaderImageProperty, value);
        public static object GetHeaderImage(Control control) => control.GetValue(HeaderImageProperty);


        public static readonly DependencyProperty HeaderImageStyleProperty = DependencyProperty.RegisterAttached("HeaderImageStyle", typeof(Style), typeof(Control), new PropertyMetadata(DefaultImageStyle, PropertyChanged));
        public static void SetHeaderImageStyle(Control control, Style value) => control.SetValue(HeaderImageStyleProperty, value);
        public static Style GetHeaderImageStyle(Control control) => (Style)control.GetValue(HeaderImageStyleProperty);


        /*public static readonly DependencyProperty BorderThicknessProperty = DependencyProperty.RegisterAttached("BorderThickness", typeof(Thickness), typeof(Control), new PropertyMetadata(new Thickness(), PropertyChanged));
        public static void SetBorderThickness(Control control, Thickness value) => control.SetValue(BorderThicknessProperty, value);
        public static Thickness GetBorderThickness(Control control) => (Thickness)control.GetValue(BorderThicknessProperty);
        */

        private static readonly DependencyProperty AlreadyDoneProperty = DependencyProperty.RegisterAttached("AlreadyDone", typeof(bool), typeof(Control), new PropertyMetadata(false, PropertyChanged));
        private static readonly DependencyProperty AlreadyDoneLockerProperty = DependencyProperty.RegisterAttached("AlreadyDoneLocker", typeof(object), typeof(Control), new PropertyMetadata(new object(), PropertyChanged));

        private static void PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //lock (d.GetValue(AlreadyDoneLockerProperty))
            {
                if ((bool)d.GetValue(AlreadyDoneProperty))
                {
                    return;
                }

                d.SetValue(AlreadyDoneProperty, true);
            }

            if ((bool)d.GetValue(PreserveTemplateProperty))
            {
                return;
            }

            Attach(d);
        }

        private static void Attach(DependencyObject d)
        {
            var control = (Control)d;

            control.Template = template;
            control.ApplyTemplate();
        }

        private CustomAdorner()
        {
            InitializeComponent();
        }
    }
}
