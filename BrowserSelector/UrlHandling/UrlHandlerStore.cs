using BrowserSelector.Configuration;

namespace BrowserSelector.UrlHandling;

public class UrlHandlerStore(IUserOptionsStore userOptionsStore) : IUrlHandlerStore
{
    public UrlHandler GetHandler(string id) =>
        userOptionsStore.Options.UrlHandlers.FirstOrDefault(h => h.Id == id)
            ?? throw new ArgumentException("No such handler is known", nameof(id));

    public IEnumerable<UrlHandler> GetHandlers() => userOptionsStore.Options.UrlHandlers;
}
