using System.Windows.Media;
using BrowserSelector.UrlHandling;
using EssentialMVVM;

namespace BrowserSelector.ViewModel;

public class UrlHandlerViewModel(UrlHandler urlHandler, ImageSource? icon) : BindableBase
{
    public string Id { get; } = urlHandler.Id;
    public string Name { get; } = urlHandler.Name;
    public ImageSource? Icon { get; } = icon;
}
