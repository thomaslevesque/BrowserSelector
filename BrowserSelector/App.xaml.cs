using System.Windows;
using BrowserSelector.Browsers;
using BrowserSelector.UrlHandling;

namespace BrowserSelector;

public partial class App(IUrlHandlerResolver urlHandlerResolver, IBrowserFactory browserFactory)
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        if (e.Args.Any() && Uri.TryCreate(e.Args[0], UriKind.Absolute, out var uri))
        {
            OpenUrl(uri);
        }
    }

    private void OpenUrl(Uri uri)
    {
        if (TryOpenUrlUsingRules(uri))
            return;

        // Show picker
        MessageBox.Show("I don't know how to open this URL.");
    }

    private bool TryOpenUrlUsingRules(Uri uri)
    {
        var urlHandler = urlHandlerResolver.TryResolve(uri);
        if (urlHandler is null)
            return false;

        urlHandler.Open(uri, browserFactory);
        return true;
    }
}
