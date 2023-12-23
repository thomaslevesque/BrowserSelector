using BrowserSelector;
using BrowserSelector.Browsers;
using BrowserSelector.UrlHandling;
using BrowserSelector.View;
using Dapplo.Microsoft.Extensions.Hosting.Wpf;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddSingleton<IMappingRuleStore, MappingRuleStore>();
        services.AddSingleton<IUrlHandlerStore, UrlHandlerStore>();
        services.AddTransient<IBrowserFactory, BrowserFactory>();
        services.AddTransient<IUrlHandlerResolver, UrlHandlerResolver>();
    })
    .ConfigureWpf(builder =>
    {
        builder.UseApplication<App>();
        builder.UseWindow<MainWindow>();
        builder.UseWindow<BrowserPickerWindow>();
    })
    .UseWpfLifetime()
    .Build();

await host.RunAsync();
