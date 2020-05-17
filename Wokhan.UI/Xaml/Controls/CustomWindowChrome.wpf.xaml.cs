using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shell;
using System.ComponentModel;
using System.Windows.Markup;

namespace Wokhan.UI.Controls
{
    [ContentProperty(nameof(Children))]
    public partial class CustomWindowChrome : UserControl, INotifyPropertyChanged
    {
        public bool IsWindowMaximized => window?.WindowState.HasFlag(WindowState.Maximized) ?? false;

        public UIElementCollection Children { get => (UIElementCollection)GetValue(ChildrenProperty); private set => SetValue(ChildrenProperty, value); }

        private const int MaximizedMargin = 7;

        public static readonly DependencyProperty ChildrenProperty = DependencyProperty.Register(nameof(Children), typeof(UIElementCollection), typeof(CustomWindowChrome));

        public ImageSource Icon { get => (ImageSource)GetValue(IconProperty); set => SetValue(IconProperty, value); }
        public static readonly DependencyProperty IconProperty = DependencyProperty.Register(nameof(Icon), typeof(ImageSource), typeof(CustomWindowChrome));

        public Style IconStyle { get => (Style)GetValue(IconStyleProperty); set => SetValue(IconStyleProperty, value); }
        public static readonly DependencyProperty IconStyleProperty = DependencyProperty.Register(nameof(IconStyle), typeof(Style), typeof(CustomWindowChrome));

        public string Caption { get => (string)GetValue(CaptionProperty); set => SetValue(CaptionProperty, value); }
        public static readonly DependencyProperty CaptionProperty = DependencyProperty.Register(nameof(Caption), typeof(string), typeof(CustomWindowChrome));

        public Style CaptionStyle { get => (Style)GetValue(CaptionStyleProperty); set => SetValue(CaptionStyleProperty, value); }
        public static readonly DependencyProperty CaptionStyleProperty = DependencyProperty.Register(nameof(CaptionStyle), typeof(Style), typeof(CustomWindowChrome));

        public Style MinimizeButtonStyle { get => (Style)GetValue(MinimizeButtonStyleProperty); set => SetValue(MinimizeButtonStyleProperty, value); }
        public static readonly DependencyProperty MinimizeButtonStyleProperty = DependencyProperty.Register(nameof(MinimizeButtonStyle), typeof(Style), typeof(CustomWindowChrome));

        public Style MaximizeButtonStyle { get => (Style)GetValue(MaximizeButtonStyleProperty); set => SetValue(MaximizeButtonStyleProperty, value); }
        public static readonly DependencyProperty MaximizeButtonStyleProperty = DependencyProperty.Register(nameof(MaximizeButtonStyle), typeof(Style), typeof(CustomWindowChrome));

        public Style CloseButtonStyle { get => (Style)GetValue(CloseButtonStyleProperty); set => SetValue(CloseButtonStyleProperty, value); }
        public static readonly DependencyProperty CloseButtonStyleProperty = DependencyProperty.Register(nameof(CloseButtonStyle), typeof(Style), typeof(CustomWindowChrome));

        private Window window;

        private WindowChrome Chrome;
        private Thickness prevThickness;

        public event PropertyChangedEventHandler PropertyChanged;

        public CustomWindowChrome() : base()
        {
            this.Loaded += CustomWindowChrome_Loaded;

            InitializeComponent();

            Children = ChildrenHost.Children;
        }

        private void CustomWindowChrome_Loaded(object sender, RoutedEventArgs e)
        {
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                window = Window.GetWindow(this);

                Chrome = new WindowChrome() { ResizeBorderThickness = new Thickness(1), CaptionHeight = Height, UseAeroCaptionButtons = false };
                WindowChrome.SetWindowChrome(window, Chrome);

                window.StateChanged += Window_StateChanged;

                window.CommandBindings.Add(new CommandBinding(SystemCommands.CloseWindowCommand, (s, e) => SystemCommands.CloseWindow(window)));
                window.CommandBindings.Add(new CommandBinding(SystemCommands.MaximizeWindowCommand, (s, e) => SystemCommands.MaximizeWindow(window)));
                window.CommandBindings.Add(new CommandBinding(SystemCommands.MinimizeWindowCommand, (s, e) => SystemCommands.MinimizeWindow(window)));
                window.CommandBindings.Add(new CommandBinding(SystemCommands.RestoreWindowCommand, (s, e) => SystemCommands.RestoreWindow(window)));
            }
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsWindowMaximized)));
            var parent = ((FrameworkElement)this.Parent);
            if (IsWindowMaximized)
            {
                prevThickness = parent.Margin;
                parent.Margin = new Thickness(MaximizedMargin);
            }
            else if (prevThickness != null)
            {
                parent.Margin = prevThickness;
            }
        }
    }
}
