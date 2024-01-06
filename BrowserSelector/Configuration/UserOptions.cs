using BrowserSelector.UrlHandling;

namespace BrowserSelector.Configuration;

public class UserOptions
{
    public IList<UrlHandler> UrlHandlers { get; set; } = new List<UrlHandler>();
    public IList<MappingRule> MappingRules { get; set; } = new List<MappingRule>();
}
