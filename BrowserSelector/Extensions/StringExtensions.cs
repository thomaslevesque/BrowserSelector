namespace BrowserSelector.Extensions;

public static class StringExtensions
{
    public static string EnsureTrailingSlash(this string path)
    {
        if (path[^1] != '/')
        {
            path += "/";
        }

        return path;
    }
}
