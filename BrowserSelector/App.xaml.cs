using System.Configuration;
using System.Data;
using System.Windows;
using BrowserSelector.Model;

namespace BrowserSelector;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var browserFactory = new BrowserFactory();
        var browsers = browserFactory.GetBrowsers().ToList();
        foreach (var browser in browsers)
        {
            Console.WriteLine($"{browser.Name}");
            if (browser is IBrowserWithProfiles browserWithProfiles)
            {
                foreach (var profile in browserWithProfiles.GetProfiles())
                {
                    Console.WriteLine($"  {profile.Id} - {profile.DisplayName}");
                }
            }
        }
        var mappingRulesStore = new MappingRuleStore();
        var urlHandlerStore = new UrlHandlerStore();
        var resolver = new UrlHandlerResolver(browserFactory, mappingRulesStore, urlHandlerStore);
        //var url = "https://github.com/FakeItEasy/FakeItEasy";
        //var url = "https://github.com/ueat/ueat";
        //var url = "https://ueatio.atlassian.net/";
        var url = "https://members.medaviebc.ca/";
        var handler = resolver.TryResolve(url);
        handler?.Open(url, browserFactory);
    }
}
