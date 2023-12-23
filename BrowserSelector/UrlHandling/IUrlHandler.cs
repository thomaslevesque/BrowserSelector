using BrowserSelector.Browsers;

namespace BrowserSelector.UrlHandling;

public interface IUrlHandler
{
    void Open(Uri uri, IBrowserFactory browserFactory);
}
