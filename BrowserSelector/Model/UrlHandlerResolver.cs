namespace BrowserSelector.Model;

public class UrlHandlerResolver(
    IBrowserFactory browserFactory,
    IMappingRuleStore mappingRuleStore,
    IUrlHandlerStore urlHandlerStore)
    : IHandlerResolver
{
    public IUrlHandler? TryResolve(string url)
    {
        var rules = mappingRuleStore.GetRules();
        foreach (var rule in rules)
        {
            if (rule.Matcher.IsMatch(url))
            {
                var handler = urlHandlerStore.GetHandler(rule.HandlerId);
                return handler;
            }
        }

        return null;
    }
}
