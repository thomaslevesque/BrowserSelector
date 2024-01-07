using System.IO;
using System.Text.Json;
using BrowserSelector.Browsers;
using BrowserSelector.UrlHandling;

namespace BrowserSelector.Configuration;

public class UserOptionsStore : IUserOptionsStore
{
    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        WriteIndented = true,
        ReadCommentHandling = JsonCommentHandling.Skip,
        AllowTrailingCommas = true
    };
    
    private readonly IBrowserFactory _browserFactory;
    private readonly Lazy<UserOptions> _options;

    public UserOptionsStore(IBrowserFactory browserFactory)
    {
        _browserFactory = browserFactory;
        _options = new Lazy<UserOptions>(Load);
    }

    public UserOptions Options => _options.Value;

    public void Save() => Save(Options);

    private UserOptions Load()
    {
        try
        {
            var optionsJson = File.ReadAllText(GetOptionsPath());
            var options = JsonSerializer.Deserialize<UserOptions>(optionsJson, SerializerOptions);
            if (options is not null)
                return options;
        }
        catch (DirectoryNotFoundException)
        {
        }
        catch (FileNotFoundException)
        {
        }
        
        var defaultOptions = CreateDefaultOptions();
        Save(defaultOptions);
        return defaultOptions;
    }

    private UserOptions CreateDefaultOptions()
    {
        var handlers = new List<UrlHandler>();
        var browsers = _browserFactory.GetAvailableBrowsers();
        foreach (var browser in browsers)
        {
            var profileSuffix = browser is IBrowserWithProfiles ? "-default" : "";
            handlers.Add(new UrlHandler
            {
                Id = browser.BaseHandlerId + profileSuffix,
                Name = browser.Name,
                BrowserId = browser.Id
            });
        }
        
        var options = new UserOptions
        {
            UrlHandlers = handlers
        };

        return options;
    }

    private static void Save(UserOptions options)
    {
        Directory.CreateDirectory(GetAppDataPath());
        var optionsJson = JsonSerializer.Serialize(options, SerializerOptions);
        File.WriteAllText(GetOptionsPath(), optionsJson);
    }

    private static string GetOptionsPath() => Path.Combine(GetAppDataPath(), "options.json");

    private static string GetAppDataPath()
    {
        var userAppDataDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        return Path.Combine(userAppDataDir, "BrowserSelector");
    }
}
