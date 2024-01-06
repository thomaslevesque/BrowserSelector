namespace BrowserSelector.UrlHandling;

public interface IUrlHandlerResolver
{
    public UrlHandler? TryResolve(Uri uri);
}
