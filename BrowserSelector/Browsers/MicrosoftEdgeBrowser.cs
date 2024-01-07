using System.IO;
using Microsoft.Win32;

namespace BrowserSelector.Browsers;

public class MicrosoftEdgeBrowser(string id, string name, string executablePath, string userDataPath)
    : ChromiumBasedBrowserBase(id, name, executablePath, userDataPath)
{
    public override string BaseHandlerId => "edge";

    public static MicrosoftEdgeBrowser? TryCreate(RegistryKey registryKey)
    {
        if (!TryGetNameAndPath(registryKey, out var name, out var executablePath))
            return null;

        var userDataPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "Microsoft",
            "Edge",
            "User Data");

        var keyName = Path.GetFileName(registryKey.Name);
        return new MicrosoftEdgeBrowser(keyName, name, executablePath, userDataPath);
    }
}
