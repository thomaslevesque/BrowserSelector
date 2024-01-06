using BrowserSelector.Browsers;
using BrowserSelector.Configuration;
using BrowserSelector.View;
using BrowserSelector.ViewModel;

namespace BrowserSelector.UrlHandling;

public class UrlOpener(
    IUrlHandlerResolver urlHandlerResolver,
    IUrlHandlerStore urlHandlerStore,
    IBrowserFactory browserFactory,
    IViewModelFactory viewModelFactory,
    IUserOptionsStore userOptionsStore) : IUrlOpener
{
    public void OpenUrl(Uri uri)
    {
        if (TryOpenUrlAutomatically(uri))
            return;

        // Show picker
        var vm = viewModelFactory.CreateBrowserPickerViewModel(uri);
        var window = new BrowserPickerWindow
        {
            DataContext = vm
        };

        if (window.ShowDialog() is true)
        {
            var urlHandler = urlHandlerStore.GetHandler(vm.SelectedHandlerId);
            if (vm.RememberMyChoice)
            {
                SaveHandlerChoice(vm);
            }
            urlHandler.Open(uri, browserFactory);
        }
    }

    private void SaveHandlerChoice(BrowserPickerViewModel vm)
    {
        var suggestion = vm.SelectedMatcherSuggestion;
        var urlMatcher = new UrlMatcher
        {
            MatchType = suggestion.MatchType,
            Value = suggestion.Value
        };
        var rule = new MappingRule
        {
            Matcher = urlMatcher,
            HandlerId = vm.SelectedHandlerId
        };
        userOptionsStore.Options.MappingRules.Add(rule);
        userOptionsStore.Save();
    }

    private bool TryOpenUrlAutomatically(Uri uri)
    {
        var urlHandler = urlHandlerResolver.TryResolve(uri);
        if (urlHandler is null)
            return false;

        urlHandler.Open(uri, browserFactory);
        return true;
    }
}
