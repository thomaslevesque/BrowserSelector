namespace BrowserSelector.Model;

public interface IUrlHandlerStore
{
    IUrlHandler GetHandler(string id);
}
