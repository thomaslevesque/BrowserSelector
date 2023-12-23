namespace BrowserSelector.Model;

public class UrlHandlerStore : IUrlHandlerStore
{
    public IUrlHandler GetHandler(string id)
    {
        return id switch
        {
            "chrome-work" => new UrlHandler
            {
                Id = id,
                BrowserId = "Google Chrome",
                ProfileId = "Profile 7"
            },
            "chrome-personal" => new UrlHandler
            {
                Id = id,
                BrowserId = "Google Chrome",
                ProfileId = "Profile 1"
            },
            "firefox-personal" => new UrlHandler
            {
                Id = id,
                BrowserId = "Firefox-308046B0AF4A39CB",
                ProfileId = "default-release"
            },
            _ => throw new ArgumentException("No such handler is known", nameof(id))
        };
    }
}
