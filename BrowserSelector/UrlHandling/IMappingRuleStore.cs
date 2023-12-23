namespace BrowserSelector.UrlHandling;

public interface IMappingRuleStore
{
    IEnumerable<MappingRule> GetRules();
}
