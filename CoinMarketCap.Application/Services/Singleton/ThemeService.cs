using System.Collections.ObjectModel;
using System.Windows;

namespace CoinMarketCap.Application.Services.Singleton;

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
public class ResourceUriPathAttribute(string path) : Attribute
{
    public string Path { get; } = path;
}

[Serializable]
public class ChangeThemeException(string message, Exception inner) : Exception(message, inner);

public class ThemeService<T>(Collection<ResourceDictionary> mergedDictionaries)
    where T : struct, Enum
{
    public ThemeService() : this(System.Windows.Application.Current.Resources.MergedDictionaries)
    {
    }

    public Collection<ResourceDictionary> ThemeSource { get; } = mergedDictionaries ?? throw new ArgumentNullException(nameof(mergedDictionaries), "Source of the themes cannot be null");

    /// <exception cref="System.IO.IOException"/>
    /// <exception cref="UriFormatException"/>
    private static ResourceDictionary CreateResourceDictionary(string uriStr)
    {
        return new ResourceDictionary()
        {
            Source = new Uri(uriStr, UriKind.RelativeOrAbsolute)
        };
    }

    private static bool IsResourceDictionaryExists(string uriStr)
    {
        if (string.IsNullOrEmpty(uriStr))
        {
            return false;
        }

        try
        {
            _ = CreateResourceDictionary(uriStr);

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    private static string GetUriPath(T theme)
    {
        return typeof(T)
            .GetMember(theme.ToString())
            .FirstOrDefault(m => m.DeclaringType == typeof(T))
            ?.GetCustomAttributes(typeof(ResourceUriPathAttribute), false)
            .Select(item => ((ResourceUriPathAttribute)item).Path)
            .FirstOrDefault();
    }

    private static IEnumerable<(T Theme, string Uri)> GetThemesData()
    {
        var themes = (T[])Enum.GetValues(typeof(T));

        return themes
            .Select(theme => (theme, GetUriPath(theme)))
            .ToList();
    }

    private static IEnumerable<(T Theme, string Uri)> GetValidThemesData()
    {
        return GetThemesData()
            .Where(item => IsResourceDictionaryExists(item.Uri))
            .Select(item => (item.Theme, item.Uri))
            .ToList();
    }

    private static IEnumerable<(T Theme, string Uri)> GetThemesThatDoNotExist()
    {
        return GetThemesData()
            .Where(item => !IsResourceDictionaryExists(item.Uri))
            .Select(item => (item.Theme, item.Uri))
            .ToList();
    }

    private (T Theme, int Index, string Uri)? GetActualThemeData()
    {
        if (ThemeSource != null)
        {
            var themesData = GetValidThemesData();

            for (int i = 0; i < ThemeSource.Count; i++)
            {
                foreach (var themeData in themesData)
                {
                    if (ThemeSource[i].Source.OriginalString == themeData.Uri)
                    {
                        return (themeData.Theme, i, themeData.Uri);
                    }
                }
            }
        }

        return null;
    }

    /// <exception cref="ChangeThemeException"/>
    public void ChangeTheme(T theme)
    {
        try
        {
            var actual = GetActualThemeData();

            var uriStr = GetUriPath(theme);

            var newTheme = CreateResourceDictionary(uriStr);

            if (actual.HasValue)
            {
                ThemeSource[actual.Value.Index] = newTheme;
            }
            else
            {
                ThemeSource.Add(newTheme);
            }
        }
        catch (System.IO.IOException exc)
        {
            throw new ChangeThemeException($"Problem with theme source. {exc.Message}", exc);
        }
        catch (UriFormatException exc)
        {
            throw new ChangeThemeException("Theme source URI path format is invalid.", exc);
        }
        catch (Exception exc)
        {
            throw new ChangeThemeException("Undefined problem during theme changing.", exc);
        }
    }

    public void RemoveTheme()
    {
        var actual = GetActualThemeData();

        if (actual.HasValue)
        {
            ThemeSource.RemoveAt(actual.Value.Index);
        }
    }

    public T? GetActualTheme()
    {
        var actual = GetActualThemeData();

        return actual?.Theme;
    }

    public IEnumerable<T> GetAvailableThemes()
    {
        return GetValidThemesData()
            .Select(data => data.Theme)
            .ToList();
    }

    public Dictionary<T, string> GetIncorrectThemes()
    {
        var themes = GetThemesThatDoNotExist();

        var result = new Dictionary<T, string>();

        foreach (var theme in themes)
        {
            result.Add(theme.Theme, $"The resource at '{theme.Uri}' of the theme '{theme.Theme}' doesn't exist");
        }

        return result;
    }
}