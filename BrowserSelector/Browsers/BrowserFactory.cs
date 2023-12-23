using System.IO;
using Microsoft.Win32;

namespace BrowserSelector.Browsers;

public class BrowserFactory : IBrowserFactory
{
    const string StartMenuInternetKeyPath = @"Software\Clients\StartMenuInternet";

    public IEnumerable<IBrowser> GetBrowsers()
    {
        using var registryKey = Registry.LocalMachine.OpenSubKey(StartMenuInternetKeyPath);
        if (registryKey == null)
            yield break;

        foreach (var subKeyName in registryKey.GetSubKeyNames())
        {
            using var subKey = registryKey.OpenSubKey(subKeyName);
            if (subKey == null)
                continue;

            var browser = TryCreate(subKey);
            if (browser != null)
                yield return browser;
        }
    }

    public IBrowser GetBrowser(string id)
    {
        using var registryKey = Registry.LocalMachine.OpenSubKey(StartMenuInternetKeyPath);
        if (registryKey == null)
            throw new InvalidOperationException("Cannot find StartMenuInternet registry key");

        using var subKey = registryKey.OpenSubKey(id);
        if (subKey == null)
            throw new InvalidOperationException($"Cannot find registry key for {id}");

        return TryCreate(subKey)
               ?? throw new InvalidOperationException($"Cannot create browser with id {id}");
    }

    private static IBrowser? TryCreate(RegistryKey registryKey)
    {
        var keyName = Path.GetFileName(registryKey.Name);
        return keyName switch
        {
            "Google Chrome" => GoogleChromeBrowser.TryCreate(registryKey),
            "Microsoft Edge" => MicrosoftEdgeBrowser.TryCreate(registryKey),
            "Chromium" => ChromiumBrowser.TryCreate(registryKey),
            _ when keyName == "Firefox" || keyName.StartsWith("Firefox-") => FirefoxBrowser.TryCreate(registryKey),
            _ => GenericBrowser.TryCreate(registryKey)
        };
    }
}
