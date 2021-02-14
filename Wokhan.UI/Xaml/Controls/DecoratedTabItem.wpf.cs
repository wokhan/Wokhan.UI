
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Effects;

namespace Wokhan.UI.Xaml.Controls
{
    /// <summary>
    /// Logique d'interaction pour DecoratedTabItem.xaml
    /// </summary>
    public class DecoratedTabItem : TabItem
    {
        [Bindable(true)]
        public Effect BackgroundEffect { get; set; }
        public static readonly DependencyProperty BackgroundEffectProperty = DependencyProperty.Register(nameof(BackgroundEffect), typeof(Effect), typeof(DecoratedTabItem));

        [Bindable(true)]
        public CornerRadius CornerRadius { get; set; }
        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(DecoratedTabItem));

        [Bindable(true)]
        public object Image { get; set; }
        public static DependencyProperty ImageProperty = DependencyProperty.Register(nameof(Image), typeof(object), typeof(DecoratedTabItem));

        static DecoratedTabItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DecoratedTabItem), new FrameworkPropertyMetadata(typeof(DecoratedTabItem)));
        }
    }
}
