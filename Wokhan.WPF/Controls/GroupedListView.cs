using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;

namespace Wokhan.WPF.Controls
{
    public class GroupedListView : ListView
    {
        [Bindable(true, BindingDirection.OneWay)]
        public string GroupBy
        {
            get => (string)GetValue(GroupByProperty);
            set => SetValue(GroupByProperty, value);
        }

        public static readonly DependencyProperty GroupByProperty = DependencyProperty.Register(nameof(GroupBy), typeof(string), typeof(GroupedListView), new PropertyMetadata(GroupByProperty_Changed));

        private static void GroupByProperty_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((GroupedListView)d).Rebind();
        }

        private void Rebind()
        {
            var view = (CollectionView)CollectionViewSource.GetDefaultView(this.ItemsSource);
            view.GroupDescriptions.Add(new PropertyGroupDescription(GroupBy));
            this.ItemsSource = view;
        }

        private static void ItemsSourceProperty_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((GroupedListView)d).Rebind();
        }

        public GroupedListView() : base()
        {
            ItemsSourceProperty.OverrideMetadata(typeof(GroupedListView), new PropertyMetadata(ItemsSourceProperty_Changed));
            //    if (this.GroupStyle.Count == 0) {
            //        var containerStyle = new Style()
            //        {
            //            TargetType = typeof(GroupItem)
            //        };
            //        XamlReader.Parse()
            //        containerStyle.Setters.Add(new Setter(TemplateProperty, new ControlTemplate()))
            //        this.GroupStyle.Add(new GroupStyle() {  ContainerStyle =  }
            //    )
            Rebind();
        }
    }
}
