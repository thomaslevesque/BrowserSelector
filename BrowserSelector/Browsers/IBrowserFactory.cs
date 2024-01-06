namespace BrowserSelector.Browsers;

public interface IBrowserFactory
{
    IEnumerable<IBrowser> GetAvailableBrowsers();
    IBrowser GetBrowser(string id);
}
