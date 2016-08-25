using System;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace Learn.Converters
{
    public class HexToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null)
            {
                var color = (string)value;
                if (color != "")
                {
                    return new SolidColorBrush(Color.FromArgb(255,
                                                              System.Convert.ToByte(color.Substring(1, 2), 16),
                                                              System.Convert.ToByte(color.Substring(3, 2), 16),
                                                              System.Convert.ToByte(color.Substring(5, 2), 16)));
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
