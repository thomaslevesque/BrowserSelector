using BrowserSelector;
using BrowserSelector.Browsers;
using BrowserSelector.Configuration;
using BrowserSelector.Extensions;
using BrowserSelector.UrlHandling;
using BrowserSelector.View;
using BrowserSelector.ViewModel;
using Dapplo.Microsoft.Extensions.Hosting.Wpf;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Solo;

using var singleInstanceApp = new SingleInstanceApp(
    "BrowserSelector-87d35a5d-6af4-4d8b-be8c-cf35e7c39939",
    args => App.Current.Activate(args));
if (!singleInstanceApp.TryStart(args))
    return;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddSingleton<IUrlHandlerStore, UrlHandlerStore>();
        services.AddSingleton<IUserOptionsStore, UserOptionsStore>();
        services.AddSingleton<IBrowserFactory, BrowserFactory>();
        services.AddSingleton<IUrlHandlerResolver, UrlHandlerResolver>();
        services.AddSingleton<IUrlOpener, UrlOpener>();
        services.AddAutoFactory<IViewModelFactory>();
        services.AddLazyResolution();
    })
    .ConfigureWpf(builder =>
    {
        builder.UseApplication<App>();
        builder.UseWindow<UserOptionsWindow>();
    })
    .UseWpfLifetime()
    .Build();

await host.RunAsync();
