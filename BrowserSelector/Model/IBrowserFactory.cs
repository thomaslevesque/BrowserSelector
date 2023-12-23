namespace BrowserSelector.Model;

public interface IBrowserFactory
{
    IEnumerable<IBrowser> GetBrowsers();
    IBrowser GetBrowser(string id);
}
