namespace BrowserSelector.Model;

public interface IUrlHandler
{
    void Open(string url, IBrowserFactory browserFactory);
}
