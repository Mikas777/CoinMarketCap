using System.Globalization;
using System.Windows.Data;

namespace CoinMarketCap.Application.Themes.Converters;

public class TruncateDecimalConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is decimal decimalValue)
        {
            // Обрезаем значение до 6 знаков после запятой
            decimal truncatedValue = Math.Truncate(decimalValue * 1000000) / 1000000;
            return $"${truncatedValue:F6}";
        }
        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}