using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace todo_list.Converters
{
    public class SelectionToBackgroundConverter : IValueConverter
    {
        public SolidColorBrush SelectedBrush { get; set; }
        public SolidColorBrush UnselectedBrush { get; set; }

        public SelectionToBackgroundConverter()
        {
            SelectedBrush = new SolidColorBrush(Color.FromRgb(44, 41, 102)); // PrimaryDark
            UnselectedBrush = new SolidColorBrush(Colors.Transparent);
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isSelected = (bool)value;
            return isSelected ? SelectedBrush : UnselectedBrush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
