namespace BrowserSelector.Browsers;

public interface IBrowserFactory
{
    IEnumerable<IBrowser> GetBrowsers();
    IBrowser GetBrowser(string id);
}
