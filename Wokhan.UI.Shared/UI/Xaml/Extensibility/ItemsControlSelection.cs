using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Wokhan.Core.Extensions;
using Wokhan.Collections.Generic.Extensions;
using System;
#if __WPF__
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
#else
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
#endif

namespace Wokhan.Shared.UI.Xaml.Extensibility
{
    public class ItemsControlSelection
    {
        /// <summary>
        /// Identifies the attached dependency property.
        /// </summary>
        public static readonly DependencyProperty SelectedValuesProperty = DependencyProperty.RegisterAttached("SelectedValues", typeof(IList), typeof(ItemsControlSelection), new PropertyMetadata(null, SelectedValuesPropertyOnChange));

        public static IList GetSelectedValues(ListView lst)
        {
            return (IList)lst.GetValue(SelectedValuesProperty);
        }

        public static void SetSelectedValues(ListView lst, IList value)
        {
            lst.SetValue(SelectedValuesProperty, value);
        }

        /// <summary>
        /// Triggered when the bound object either changes or when its content gets updated (and notified).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void SelectedValuesPropertyOnChange(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var lst = sender as ListView;
            if (lst == null)
            {
                return;
            }

            lst.SelectionChanged -= SelectionChanged;
#if __UAP__ || __WPF__
            var path = (string)lst.GetValue(Selector.SelectedValuePathProperty);
#else
            var path = (string)lst.GetDependencyPropertyValue(nameof(Selector.SelectedValuePath));
#endif

            lst.SelectedItems.Clear();
            if (e.NewValue != null && ((ICollection)e.NewValue).Count > 0)
            {
                Func<object, object> getValue = (x) => path != null ? x.GetValueFromPath(path) : x;
                IEnumerable<object> news = ((ICollection)e.NewValue).Cast<object>().Join(lst.Items.Cast<object>(), a => a, getValue, (a, b) => b);
                lst.SelectedItems.AddRange(news);
            }

            lst.SelectionChanged += SelectionChanged;
        }

        /// <summary>
        /// Triggered when manual selection changes on the ItemsControl component
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var lst = (ListView)sender;
            // Avoid clearing the list it the source has not been populated yet
            /*if (lst.Items.Count == 0)
            {
                return;
            }*/

            IList src = GetSelectedValues(lst);
#if __UAP__ || __WPF__
            var path = (string)lst.GetValue(Selector.SelectedValuePathProperty);
#else
            var path = (string)lst.GetDependencyPropertyValue(nameof(Selector.SelectedValuePath));
#endif
            if (lst.SelectedItem != null)
            {
                src.Clear();
                src.AddRange(lst.SelectedItems.Cast<object>().Select(item => item.GetValueFromPath(path)));
            }
            //src.AddRange(e.AddedItems.Select(i => i.GetValueFromObject(path)));
            //src.RemoveRange(e.RemovedItems.Select(i => i.GetValueFromObject(path)));
            //var items = lst.SelectedItems.ToList();
            //lst.SetValue(SelectedValuesProperty, lst.SelectedItems.Select(x => x.GetValueFromObject(path)).ToList());
        }
    }
}