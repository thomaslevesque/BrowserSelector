using System.Windows;
using BrowserSelector.UrlHandling;
using BrowserSelector.View;

namespace BrowserSelector;

public partial class App
{
    private readonly IUrlOpener _urlOpener;
    private readonly Lazy<UserOptionsWindow> _userOptionsWindow;

    public App(IUrlOpener urlOpener, Lazy<UserOptionsWindow> userOptionsWindow)
    {
        InitializeComponent();
        _urlOpener = urlOpener;
        _userOptionsWindow = userOptionsWindow;
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        Activate(e.Args);
    }

    protected override void OnExit(ExitEventArgs e)
    {
        base.OnExit(e);
    }

    public void Activate(string[] args)
    {
        if (Dispatcher.CheckAccess())
            ActivateCore(args);
        else
            Dispatcher.Invoke(() => ActivateCore(args));
    }

    private void ActivateCore(string[] args)
    {
        var parsedArgs = CommandLineArgs.Parse(args);
        if (parsedArgs.Uri is not null)
        {
            _urlOpener.OpenUrl(parsedArgs.Uri);
        }
        else
        {
            var userOptionsWindow = _userOptionsWindow.Value;
            userOptionsWindow.WindowState = WindowState.Normal;
            userOptionsWindow.Show();
            userOptionsWindow.Activate();
        }
    }

    public new static App Current => (App) Application.Current;
}
