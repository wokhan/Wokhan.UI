using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shell;
using System.ComponentModel;
using System.Windows.Markup;

namespace Wokhan.UI.Xaml.Controls
{
    [ContentProperty(nameof(Children))]
    public partial class CustomWindowChrome : UserControl, INotifyPropertyChanged
    {
        public bool IsWindowMaximized => Window?.WindowState.HasFlag(WindowState.Maximized) ?? false;

        public UIElementCollection Children { get => (UIElementCollection)GetValue(ChildrenProperty); private set => SetValue(ChildrenProperty, value); }
        public static readonly DependencyProperty ChildrenProperty = DependencyProperty.Register(nameof(Children), typeof(UIElementCollection), typeof(CustomWindowChrome));

        public HorizontalAlignment TitleAlignment { get => (HorizontalAlignment)GetValue(TitleAlignmentProperty); set => SetValue(TitleAlignmentProperty, value); }
        public static readonly DependencyProperty TitleAlignmentProperty = DependencyProperty.Register(nameof(TitleAlignment), typeof(HorizontalAlignment), typeof(CustomWindowChrome), new PropertyMetadata(HorizontalAlignment.Left));

        public Style IconStyle { get => (Style)GetValue(IconStyleProperty); set => SetValue(IconStyleProperty, value); }
        public static readonly DependencyProperty IconStyleProperty = DependencyProperty.Register(nameof(IconStyle), typeof(Style), typeof(CustomWindowChrome));

        public Style TitleStyle { get => (Style)GetValue(TitleStyleProperty); set => SetValue(TitleStyleProperty, value); }
        public static readonly DependencyProperty TitleStyleProperty = DependencyProperty.Register(nameof(TitleStyle), typeof(Style), typeof(CustomWindowChrome));

        public Style MinimizeButtonStyle { get => (Style)GetValue(MinimizeButtonStyleProperty); set => SetValue(MinimizeButtonStyleProperty, value); }
        public static readonly DependencyProperty MinimizeButtonStyleProperty = DependencyProperty.Register(nameof(MinimizeButtonStyle), typeof(Style), typeof(CustomWindowChrome));

        public Style MaximizeButtonStyle { get => (Style)GetValue(MaximizeButtonStyleProperty); set => SetValue(MaximizeButtonStyleProperty, value); }
        public static readonly DependencyProperty MaximizeButtonStyleProperty = DependencyProperty.Register(nameof(MaximizeButtonStyle), typeof(Style), typeof(CustomWindowChrome));

        public Style CloseButtonStyle { get => (Style)GetValue(CloseButtonStyleProperty); set => SetValue(CloseButtonStyleProperty, value); }
        public static readonly DependencyProperty CloseButtonStyleProperty = DependencyProperty.Register(nameof(CloseButtonStyle), typeof(Style), typeof(CustomWindowChrome));

        public Window Window { get; private set; }

        private const int MaximizedMargin = 7;
        private WindowChrome Chrome;
        private Thickness prevThickness;

        public event PropertyChangedEventHandler PropertyChanged;

        public CustomWindowChrome() : base()
        {
            this.DataContext = this;
            this.Loaded += CustomWindowChrome_Loaded;

            InitializeComponent();

            Children = ChildrenHost.Children;
        }

        private void CustomWindowChrome_Loaded(object sender, RoutedEventArgs e)
        {
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                Window = Window.GetWindow(this);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Window)));

                Chrome = new WindowChrome() { ResizeBorderThickness = new Thickness(1), CaptionHeight = Height, UseAeroCaptionButtons = false };
                WindowChrome.SetWindowChrome(Window, Chrome);

                Window.StateChanged += Window_StateChanged;

                Window.CommandBindings.Add(new CommandBinding(SystemCommands.CloseWindowCommand, (s, e) => SystemCommands.CloseWindow(Window)));
                Window.CommandBindings.Add(new CommandBinding(SystemCommands.MaximizeWindowCommand, (s, e) => SystemCommands.MaximizeWindow(Window)));
                Window.CommandBindings.Add(new CommandBinding(SystemCommands.MinimizeWindowCommand, (s, e) => SystemCommands.MinimizeWindow(Window)));
                Window.CommandBindings.Add(new CommandBinding(SystemCommands.RestoreWindowCommand, (s, e) => SystemCommands.RestoreWindow(Window)));
            }
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsWindowMaximized)));
            var parent = (FrameworkElement)this.Parent;
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
