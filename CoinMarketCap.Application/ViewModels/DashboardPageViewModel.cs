using System.Collections.ObjectModel;
using System.Reactive.Linq;
using CoinMarketCap.Application.Models;
using CoinMarketCap.Application.Common;
using CoinMarketCap.Application.Services.Singleton.Interfaces;
using CommunityToolkit.Mvvm.Input;
using DynamicData.Binding;
using CommunityToolkit.Mvvm.ComponentModel;
using DynamicData;
using ReactiveUI;
using CoinMarketCap.Application.Common.Enums;
using CoinMarketCap.Application.Services.Singleton;
using System.Windows.Media.Imaging;
using CoinMarketCap.Application.Services.Transient.Interfaces;

namespace CoinMarketCap.Application.ViewModels;

public partial class DashboardPageViewModel : PageViewModelBase
{
    private readonly INavigationService _navigationService;
    private readonly ReadOnlyObservableCollection<Cryptocurrency> _cryptocurrencies;
    private readonly ThemeService<ApplicationTheme> _themeService;
    private readonly IPlotService _plotService;

    [ObservableProperty]
    private string? _searchText;

    [ObservableProperty]
    private string? _sortColumn;

    [ObservableProperty]
    private bool _sortAscending = true;

    public ReadOnlyObservableCollection<Cryptocurrency> Cryptocurrencies => _cryptocurrencies;

    public DashboardPageViewModel(RuntimeDataStorage runtimeDataStorage, LanguageService languageService, INavigationService navigationService, IPlotService plotService)
    {
        languageService.ChangeLanguage("uk-UA");

        _navigationService = navigationService;
        _plotService = plotService;

        _themeService = new ThemeService<ApplicationTheme>();
        _themeService.ChangeTheme(ApplicationTheme.Light);

        var filter = this.WhenAnyValue(vm => vm.SearchText)
        .Throttle(TimeSpan.FromMilliseconds(300))
        .DistinctUntilChanged()
        .Select(BuildFilter);

        var sort = this.WhenAnyValue(vm => vm.SortColumn, vm => vm.SortAscending)
            .Select(tuple => BuildSort(tuple.Item1, tuple.Item2));

        runtimeDataStorage.Cryptocurrencies.ToObservableChangeSet()
            .Filter(filter)
            .Sort(sort)
            .ObserveOn(RxApp.MainThreadScheduler)
            .Bind(out _cryptocurrencies).Subscribe();
    }

    [RelayCommand]
    private void ToggleTheme()
    {
        var currentTheme = _themeService.GetActualTheme();

        _themeService.ChangeTheme(currentTheme == ApplicationTheme.Light
            ? ApplicationTheme.Dark
            : ApplicationTheme.Light);
    }

    [RelayCommand]
    private async Task OpenCryptocurrencyDetail(Cryptocurrency cryptocurrency)
    {
        if (cryptocurrency is null)
        {
            return;
        }

        var imageUrl = $"https://assets.coincap.io/assets/icons/{cryptocurrency.Symbol.ToLower()}@2x.png";
        var cryptocurrencyImage = new BitmapImage(new Uri(imageUrl));

        var pointsPlot = await _plotService.GeneratePlotAsync(cryptocurrency.Name, cryptocurrency.Id).ConfigureAwait(false);

        var detailViewModel = new CryptocurrencyDetailViewModel(cryptocurrency, _navigationService, cryptocurrencyImage, pointsPlot);

        _navigationService.NavigateTo(detailViewModel);
    }

    private Func<Cryptocurrency, bool> BuildFilter(string? searchText)
    {
        if (string.IsNullOrWhiteSpace(searchText))
        {
            return _ => true;
        }

        return item =>
            item.Symbol.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
            item.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase);
    }

    private IComparer<Cryptocurrency> BuildSort(string? sortColumn, bool sortAscending)
    {
        if (string.IsNullOrWhiteSpace(sortColumn))
        {
            return SortExpressionComparer<Cryptocurrency>.Ascending(i => i.Rank);
        }

        return sortColumn switch
        {
            "Rank" => sortAscending
                ? SortExpressionComparer<Cryptocurrency>.Ascending(i => i.Rank)
                : SortExpressionComparer<Cryptocurrency>.Descending(i => i.Rank),
            "Name" => sortAscending
                ? SortExpressionComparer<Cryptocurrency>.Ascending(i => i.Name)
                : SortExpressionComparer<Cryptocurrency>.Descending(i => i.Name),
            "Price" => sortAscending
                ? SortExpressionComparer<Cryptocurrency>.Ascending(i => i.PriceUsd)
                : SortExpressionComparer<Cryptocurrency>.Descending(i => i.PriceUsd),
            "MarketCap" => sortAscending
                ? SortExpressionComparer<Cryptocurrency>.Ascending(i => i.MarketCapUsd)
                : SortExpressionComparer<Cryptocurrency>.Descending(i => i.MarketCapUsd),
            "Vwap24Hr" => sortAscending
                ? SortExpressionComparer<Cryptocurrency>.Ascending(i => i.Vwap24Hr)
                : SortExpressionComparer<Cryptocurrency>.Descending(i => i.Vwap24Hr),
            "Supply" => sortAscending
                ? SortExpressionComparer<Cryptocurrency>.Ascending(i => i.Supply)
                : SortExpressionComparer<Cryptocurrency>.Descending(i => i.Supply),
            "Volume" => sortAscending
                ? SortExpressionComparer<Cryptocurrency>.Ascending(i => i.VolumeUsd24Hr)
                : SortExpressionComparer<Cryptocurrency>.Descending(i => i.VolumeUsd24Hr),
            "Change" => sortAscending
                ? SortExpressionComparer<Cryptocurrency>.Ascending(i => i.ChangePercent24Hr)
                : SortExpressionComparer<Cryptocurrency>.Descending(i => i.ChangePercent24Hr),
            _ => SortExpressionComparer<Cryptocurrency>.Ascending(i => i.Rank)
        };
    }
}