﻿using System.IO;
using Microsoft.Win32;

namespace BrowserSelector.Browsers;

public class GenericBrowser(string id, string name, string executablePath)
    : BrowserBase(id, name, executablePath)
{
    public override string BaseHandlerId => Id.ToLowerInvariant();

    public static GenericBrowser? TryCreate(RegistryKey registryKey)
    {
        if (!TryGetNameAndPath(registryKey, out var name, out var executablePath))
            return null;

        var keyName = Path.GetFileName(registryKey.Name);
        return new GenericBrowser(keyName, name, executablePath);
    }
}
