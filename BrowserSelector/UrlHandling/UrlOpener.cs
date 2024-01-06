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
            var handler = urlHandlerStore.GetHandler(vm.SelectedHandlerId);
            if (vm.RememberMyChoice)
            {
                SaveHandlerChoice(vm);
            }
            Open(uri, handler);
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
        var handler = urlHandlerResolver.TryResolve(uri);
        if (handler is null)
            return false;

        Open(uri, handler);
        return true;
    }

    private void Open(Uri uri, UrlHandler handler)
    {
        var browser = browserFactory.GetBrowser(handler.BrowserId);

        if (!string.IsNullOrEmpty(handler.ProfileId))
        {
            if (browser is IBrowserWithProfiles browserWithProfiles)
            {
                var profile = browserWithProfiles.GetProfiles().FirstOrDefault(p => p.Id == handler.ProfileId);
                if (profile is null)
                {
                    throw new InvalidOperationException($"Profile with id {handler.ProfileId} not found");
                }
                browserWithProfiles.Open(uri, profile, handler.AdditionalArguments);
                return;
            }

            throw new InvalidOperationException($"Browser {handler.BrowserId} does not support profiles");
        }

        browser.Open(uri, handler.AdditionalArguments);
    }
}
