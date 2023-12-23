using System.IO;
using BrowserSelector.Extensions;
using Microsoft.Win32;

namespace BrowserSelector.Model.Browsers;

public class FirefoxBrowser(string id, string name, string executablePath)
    : BrowserBase(id, name, executablePath), IBrowserWithProfiles
{
    public void Open(string url, BrowserProfile profile, IList<string>? additionalArguments = null)
    {
        additionalArguments =
        [
            ..additionalArguments,
            "-P",
            profile.Id.EncloseInQuotes()
        ];
        Open(url, additionalArguments);
    }

    public IEnumerable<BrowserProfile> GetProfiles()
    {
        var profilesPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "Mozilla",
            "Firefox",
            "Profiles");

        foreach (var directory in Directory.GetDirectories(profilesPath))
        {
            var rawName = Path.GetFileName(directory);
            var profileName = rawName.Split('.')[1];
            yield return new BrowserProfile(profileName, profileName);
        }
    }

    public static FirefoxBrowser? TryCreate(RegistryKey registryKey)
    {
        if (!TryGetNameAndPath(registryKey, out var name, out var executablePath))
            return null;

        var keyName = Path.GetFileName(registryKey.Name);
        return new FirefoxBrowser(keyName, name, executablePath);
    }
}
