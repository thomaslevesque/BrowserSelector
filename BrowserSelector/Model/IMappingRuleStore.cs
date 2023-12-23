namespace BrowserSelector.Model;

public interface IMappingRuleStore
{
    IEnumerable<MappingRule> GetRules();
}
