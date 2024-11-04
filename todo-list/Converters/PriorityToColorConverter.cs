// File: Converters/PriorityToColorConverter.cs
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace todo_list.Converters
{
    public class PriorityToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string priority = value as string;
            switch (priority)
            {
                case "HIGH":
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF662944")); // Red
                case "MID":
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF636629")); // Yellow
                case "LOW":
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF29664a")); // Green
                default:
                    return Brushes.Transparent;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
