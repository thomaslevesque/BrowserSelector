namespace BrowserSelector.UrlHandling;

public interface IUrlHandlerResolver
{
    public IUrlHandler? TryResolve(Uri uri);
}
