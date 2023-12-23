namespace BrowserSelector.UrlHandling;

public interface IUrlHandlerStore
{
    IUrlHandler GetHandler(string id);
}
