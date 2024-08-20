using System.Globalization;
using System.Windows.Data;

namespace CoinMarketCap.Application.Themes.Converters;

public class TruncateDecimalConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is decimal decimalValue)
        {
            var truncatedValue = Math.Truncate(decimalValue * 1000000) / 1000000;

            return truncatedValue switch
            {
                <= (decimal)0.01 => $"${truncatedValue:F6}",
                <= (decimal)0.1 => $"${truncatedValue:F5}",
                <= 1 => $"${truncatedValue:F4}",
                <= 10 => $"${truncatedValue:F3}",
                <= 100 => $"${truncatedValue:F2}",
                <= 1000 => $"${truncatedValue:F1}",
                _ => $"${truncatedValue:F2}"
            };
        }
        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}