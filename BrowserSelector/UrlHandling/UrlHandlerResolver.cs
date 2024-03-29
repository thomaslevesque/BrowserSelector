using BrowserSelector.Configuration;

namespace BrowserSelector.UrlHandling;

public class UrlHandlerResolver(
    IUserOptionsStore userOptionsStore,
    IUrlHandlerStore urlHandlerStore)
    : IUrlHandlerResolver
{
    public UrlHandler? TryResolve(Uri uri)
    {
        foreach (var rule in userOptionsStore.Options.MappingRules)
        {
            if (rule.Matcher.IsMatch(uri))
            {
                if (rule.HandlerId == UrlHandler.UnknownHandlerId)
                    return null;

                var handler = urlHandlerStore.GetHandler(rule.HandlerId);
                return handler;
            }
        }

        return null;
    }
}
