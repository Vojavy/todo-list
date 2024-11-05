using System.Collections;
using System.Windows;
using System.Windows.Controls;

namespace todo_list.Helpers
{
    public static class SelectedItemsBehavior
    {
        public static readonly DependencyProperty BindableSelectedItemsProperty =
            DependencyProperty.RegisterAttached(
                "BindableSelectedItems",
                typeof(IList),
                typeof(SelectedItemsBehavior),
                new PropertyMetadata(null, OnBindableSelectedItemsChanged));

        public static IList GetBindableSelectedItems(DependencyObject obj)
        {
            return (IList)obj.GetValue(BindableSelectedItemsProperty);
        }

        public static void SetBindableSelectedItems(DependencyObject obj, IList value)
        {
            obj.SetValue(BindableSelectedItemsProperty, value);
        }

        private static void OnBindableSelectedItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ListView listView)
            {
                if (e.OldValue != null)
                {
                    listView.SelectionChanged -= ListView_SelectionChanged;
                }

                if (e.NewValue != null)
                {
                    listView.SelectionChanged += ListView_SelectionChanged;
                }
            }
        }

        private static void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ListView listView)
            {
                IList boundList = GetBindableSelectedItems(listView);
                if (boundList == null)
                    return;

                boundList.Clear();
                foreach (var item in listView.SelectedItems)
                {
                    boundList.Add(item);
                }
            }
        }
    }
}
