using BrowserSelector.UrlHandling;
using EssentialMVVM;

namespace BrowserSelector.ViewModel;

public class UrlHandlerViewModel : BindableBase
{
    public UrlHandlerViewModel(UrlHandler urlHandler)
    {
        Id = urlHandler.Id;
        Name = urlHandler.Name;
    }

    public string Id { get; }
    public string Name { get; }
}
