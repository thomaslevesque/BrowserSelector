namespace BrowserSelector.UrlHandling;

public class UrlHandlerResolver(
    IMappingRuleStore mappingRuleStore,
    IUrlHandlerStore urlHandlerStore)
    : IUrlHandlerResolver
{
    public IUrlHandler? TryResolve(Uri uri)
    {
        var rules = mappingRuleStore.GetRules();
        foreach (var rule in rules)
        {
            if (rule.Matcher.IsMatch(uri))
            {
                var handler = urlHandlerStore.GetHandler(rule.HandlerId);
                return handler;
            }
        }

        return null;
    }
}
