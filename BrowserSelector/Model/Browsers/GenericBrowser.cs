using System.IO;
using Microsoft.Win32;

namespace BrowserSelector.Model.Browsers;

public class GenericBrowser(string id, string name, string executablePath)
    : BrowserBase(id, name, executablePath)
{
    public static GenericBrowser? TryCreate(RegistryKey registryKey)
    {
        if (!TryGetNameAndPath(registryKey, out var name, out var executablePath))
            return null;

        var keyName = Path.GetFileName(registryKey.Name);
        return new GenericBrowser(keyName, name, executablePath);
    }
}
