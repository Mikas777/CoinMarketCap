﻿using System.Windows;
using System.Windows.Controls;

namespace CoinMarketCap.Application.Themes.Controls;

public class ToggleSwitch : CheckBox
{
    static ToggleSwitch()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(ToggleSwitch), new FrameworkPropertyMetadata(typeof(ToggleSwitch)));
    }
}