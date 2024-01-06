using System.Collections.Concurrent;
using System.Reflection;
using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;

namespace BrowserSelector.Extensions;

public static class ServiceCollectionExtensions
{
    #region AutoFactory

    public static IServiceCollection AddAutoFactory<TFactory>(this IServiceCollection services)
        where TFactory : class
    {
        services.AddSingleton(CreateFactory<TFactory>);
        return services;
    }

    private static TFactory CreateFactory<TFactory>(IServiceProvider serviceProvider)
        where TFactory : class
    {
        var generator = new ProxyGenerator();
        return generator.CreateInterfaceProxyWithoutTarget<TFactory>(
            new FactoryInterceptor(serviceProvider));
    }

    private class FactoryInterceptor(IServiceProvider serviceProvider) : IInterceptor
    {
        private readonly ConcurrentDictionary<MethodInfo, ObjectFactory> _factories = new();

        public void Intercept(IInvocation invocation)
        {
            var factory = _factories.GetOrAdd(invocation.Method, CreateFactory);
            invocation.ReturnValue = factory(serviceProvider, invocation.Arguments);
        }

        private ObjectFactory CreateFactory(MethodInfo method)
        {
            return ActivatorUtilities.CreateFactory(
                method.ReturnType,
                method.GetParameters().Select(p => p.ParameterType).ToArray());
        }
    }

    #endregion

    #region LazyResolution

    public static IServiceCollection AddLazyResolution(this IServiceCollection services)
    {
        return services.AddTransient(
            typeof(Lazy<>),
            typeof(LazilyResolved<>));
    }

    private class LazilyResolved<T>(IServiceProvider serviceProvider) : Lazy<T>(serviceProvider.GetRequiredService<T>)
        where T : notnull;


    #endregion
}
