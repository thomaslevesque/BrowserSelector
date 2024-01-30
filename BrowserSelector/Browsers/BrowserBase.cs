using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Windows.Media;
using BrowserSelector.Interop;
using Microsoft.Win32;

namespace BrowserSelector.Browsers;

public abstract class BrowserBase(string id, string name, string executablePath) : IBrowser
{
    public string Id => id;
    public string Name => name;

    public string ExecutablePath => executablePath;
    
    public abstract string BaseHandlerId { get; }

    public virtual void Open(Uri uri, IList<string>? additionalArguments = null)
    {
        var psi = new ProcessStartInfo(ExecutablePath)
        {
            UseShellExecute = false
        };
        if (additionalArguments != null)
        {
            foreach (var additionalArgument in additionalArguments)
            {
                psi.ArgumentList.Add(additionalArgument);
            }
        }
        psi.ArgumentList.Add(uri.AbsoluteUri);
        Process.Start(psi);
    }

    public ImageSource? GetIcon()
    {
        return IconExtractor.GetIcon(ExecutablePath.Trim('"'));
    }

    protected static bool TryGetNameAndPath(
        RegistryKey registryKey,
        [NotNullWhen(true)] out string? name,
        [NotNullWhen(true)] out string? executablePath)
    {
        name = registryKey.GetValue(null) as string ?? Path.GetFileName(registryKey.Name);
        using var subKey = registryKey.OpenSubKey(@"shell\open\command");
        executablePath = subKey?.GetValue(null) as string;
        return !string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(executablePath);
    }
}
