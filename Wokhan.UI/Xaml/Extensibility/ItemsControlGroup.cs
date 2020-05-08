using System;
using System.ComponentModel;
using Wokhan.Collections;
using System.Collections;
#if __WPF__
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
#else
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
#endif

namespace Wokhan.UI.Xaml.Extensibility
{
    public class ItemsControlGroup
    {
        public static readonly DependencyProperty GroupByProperty = DependencyProperty.RegisterAttached("GroupBy", typeof(string), typeof(ItemsControlGroup), new PropertyMetadata(null, GroupByPropertyOnChange));

        private static void GroupByPropertyOnChange(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            var lst = dependencyObject as ListView;
            if (lst == null)
            {
                return;
            }
            
            var groupby = (string)lst.GetValue(GroupByProperty);
            InitGroup(lst, groupby);
        }

        public static readonly DependencyProperty OriginalItemsSourceProperty = DependencyProperty.RegisterAttached("OriginalItemsSource", typeof(IEnumerable), typeof(ItemsControlGroup), null);

        public static string GetGroupBy(ListView lst)
        {
            return (string)lst.GetValue(GroupByProperty);
        }

        public static void SetGroupBy(ListView lst, string value)
        {
            lst.SetValue(GroupByProperty, value);
            InitGroup(lst, value);
        }

        private static void InitGroup(ListView lst, string value)
        {
            var originalSource = lst.GetValue(OriginalItemsSourceProperty);
            if (originalSource == null)
            {
                originalSource = lst.ItemsSource;
                lst.SetValue(OriginalItemsSourceProperty, originalSource);
            }

#if __WPF__
            var view = CollectionViewSource.GetDefaultView(originalSource);
            view.GroupDescriptions.Add(new PropertyGroupDescription(value));
#else
            var view = new CollectionViewSource();
            var source = new GroupedObservableCollection<object, object>(x => x.GetType().GetProperty(value).GetValue(x));
            foreach (var x in (IEnumerable)originalSource)
            {
                source.Add(x);
            }
            view.IsSourceGrouped = true;
            view.Source = source;
#endif
            //var view = (CollectionView)CollectionViewSource.GetDefaultView(this.ItemsSource);
            //view.GroupDescriptions.Add(new PropertyGroupDescription(GroupBy));
            lst.ItemsSource = view;
        }
    }
}
