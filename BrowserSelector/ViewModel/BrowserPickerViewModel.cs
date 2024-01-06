using BrowserSelector.UrlHandling;
using EssentialMVVM;

namespace BrowserSelector.ViewModel;

public class BrowserPickerViewModel : BindableBase
{
    public Uri Url { get; }

    public BrowserPickerViewModel(Uri url, IUrlHandlerStore urlHandlerStore)
    {
        Url = url;
        Handlers = urlHandlerStore.GetHandlers()
            .Select(h => new UrlHandlerViewModel(h))
            .ToList();
        MatcherSuggestions = CreateMatcherSuggestions(url).ToList();
        _selectedMatcherSuggestion = MatcherSuggestions.First();
        SelectCommand = new DelegateCommand<string>(SelectHandler);
    }

    public IEnumerable<UrlHandlerViewModel> Handlers { get; }
    
    public IReadOnlyList<UrlMatcherSuggestion> MatcherSuggestions { get; }

    private UrlMatcherSuggestion _selectedMatcherSuggestion;
    public UrlMatcherSuggestion SelectedMatcherSuggestion
    {
        get => _selectedMatcherSuggestion;
        set => Set(ref _selectedMatcherSuggestion, value);
    }

    private bool _rememberMyChoice;
    public bool RememberMyChoice
    {
        get => _rememberMyChoice;
        set => Set(ref _rememberMyChoice, value);
    }

    public string SelectedHandlerId { get; set; } = string.Empty;

    public DelegateCommand<string> SelectCommand { get; }

    public event EventHandler HandlerSelected = null!;

    private void SelectHandler(string handlerId)
    {
        SelectedHandlerId = handlerId;
        HandlerSelected?.Invoke(this, EventArgs.Empty);
    }

    private static IEnumerable<UrlMatcherSuggestion> CreateMatcherSuggestions(Uri uri)
    {
        yield return new(UrlMatchType.Authority, uri.Authority, $"host {uri.Authority}");
        var segments = uri.Segments;
        for (int i = 0; i < segments.Length; i++)
        {
            if (!segments[i].EndsWith('/'))
                break;
            var pathPrefix = string.Concat(segments.Take(i + 1));
            if (pathPrefix == "/")
                continue;

            yield return new(UrlMatchType.AuthorityAndPath, uri.Authority + uri.AbsolutePath, $"host {uri.Authority} and path starting with {pathPrefix}");
        }
        yield return new(UrlMatchType.Exact, uri.AbsoluteUri, "this exact URL");
    }

    public record UrlMatcherSuggestion(UrlMatchType MatchType, string Value, string DisplayName);
}
