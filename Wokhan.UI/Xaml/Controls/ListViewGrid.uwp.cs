﻿using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Wokhan.UI.Xaml.Controls
{
    public sealed partial class ListViewGrid : ListView
    {
        public ListViewGrid() : base()
        {
        }

     /*   public ColumnDefinitionCollection ColumnDefinitions
        {
            get;
        }

        public RowDefinitionCollection RowDefinitions
        {
            get;
        }
        */
        /*protected override Size ArrangeOverride(Size finalSize)
        {
            return base.ArrangeOverride(finalSize);
        }*/

        /*protected override bool IsItemItsOwnContainerOverride(object item)
        {
            var r = base.IsItemItsOwnContainerOverride(item);
            return r;
        }*/

        protected override DependencyObject GetContainerForItemOverride()
        {
            if (ItemTemplate == null)
            {
                return base.GetContainerForItemOverride();
            }

            return (FrameworkElement)ItemTemplate.LoadContent();
        }

        public static ListViewGrid FromListView(Grid parent, ListView itms)
        {
            var icg = new ListViewGrid
            {
                ItemsPanel = itms.ItemsPanel,
                ItemTemplate = itms.ItemTemplate
            };
            parent.Children.Remove(itms);
            parent.Children.Add(icg);

            return icg;
        }
    }
}
