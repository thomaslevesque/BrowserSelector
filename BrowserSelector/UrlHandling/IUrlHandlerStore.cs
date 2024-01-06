namespace BrowserSelector.UrlHandling;

public interface IUrlHandlerStore
{
    UrlHandler GetHandler(string id);
    IEnumerable<UrlHandler> GetHandlers();
}
