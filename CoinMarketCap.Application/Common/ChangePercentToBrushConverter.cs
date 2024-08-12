﻿using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace CoinMarketCap.Application.Common;

public class ChangePercentToBrushConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is decimal changePercent)
        {
            return changePercent >= 0 ? Brushes.Green : Brushes.Red;
        }
        return Brushes.Black;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}