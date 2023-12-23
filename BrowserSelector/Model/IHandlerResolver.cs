namespace BrowserSelector.Model;

public interface IHandlerResolver
{
    public IUrlHandler? TryResolve(string url);
}
