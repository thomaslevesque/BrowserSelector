using System.IO;
using Microsoft.Win32;

namespace BrowserSelector.Browsers;

public class ChromiumBrowser(string id, string name, string executablePath, string userDataPath)
    : ChromiumBasedBrowserBase(id, name, executablePath, userDataPath)
{
    public static ChromiumBrowser? TryCreate(RegistryKey registryKey)
    {
        if (!TryGetNameAndPath(registryKey, out var name, out var executablePath))
            return null;

        var userDataPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "Chromium",
            "User Data");

        var keyName = Path.GetFileName(registryKey.Name);
        return new ChromiumBrowser(keyName, name, executablePath, userDataPath);
    }
}
