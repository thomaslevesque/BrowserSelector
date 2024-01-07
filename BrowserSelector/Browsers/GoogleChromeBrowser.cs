using System.IO;
using Microsoft.Win32;

namespace BrowserSelector.Browsers;

public class GoogleChromeBrowser(string id, string name, string executablePath, string userDataPath)
    : ChromiumBasedBrowserBase(id, name, executablePath, userDataPath)
{
    public override string BaseHandlerId => "chrome";

    public static GoogleChromeBrowser? TryCreate(RegistryKey registryKey)
    {
        if (!TryGetNameAndPath(registryKey, out var name, out var executablePath))
            return null;

        var userDataPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "Google",
            "Chrome",
            "User Data");

        var keyName = Path.GetFileName(registryKey.Name);
        return new GoogleChromeBrowser(keyName, name, executablePath, userDataPath);
    }
}
