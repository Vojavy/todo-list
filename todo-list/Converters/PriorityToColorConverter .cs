using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace todo_list.Converters
{
    public class PriorityToColorConverter : IValueConverter
    {
        public SolidColorBrush HighPriorityBrush { get; set; }
        public SolidColorBrush MidPriorityBrush { get; set; }
        public SolidColorBrush LowPriorityBrush { get; set; }

        public PriorityToColorConverter()
        {
            HighPriorityBrush = (SolidColorBrush)App.Current.Resources["RedBrush"];
            MidPriorityBrush = (SolidColorBrush)App.Current.Resources["YellowBrush"];
            LowPriorityBrush = (SolidColorBrush)App.Current.Resources["GreenBrush"];
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string priority = value as string;
            switch (priority)
            {
                case "HIGH":
                    return HighPriorityBrush;
                case "MID":
                    return MidPriorityBrush;
                case "LOW":
                    return LowPriorityBrush;
                default:
                    return Brushes.White;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
