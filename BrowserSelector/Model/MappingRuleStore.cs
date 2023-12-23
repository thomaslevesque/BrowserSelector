namespace BrowserSelector.Model;

public class MappingRuleStore : IMappingRuleStore
{
    public IEnumerable<MappingRule> GetRules()
    {
        return
        [
            new MappingRule
            {
                Matcher = new UrlMatcher
                {
                    MatchType = UrlMatchType.AuthorityAndPath,
                    Value = "https://github.com/ueat"
                },
                HandlerId = "chrome-work",
                Priority = 100
            },
            new MappingRule
            {
                Matcher = new UrlMatcher
                {
                    MatchType = UrlMatchType.AuthorityAndPath,
                    Value = "https://ueatio.atlassian.net/"
                },
                HandlerId = "chrome-work",
            },
            new MappingRule
            {
                Matcher = new UrlMatcher
                {
                    MatchType = UrlMatchType.AuthorityAndPath,
                    Value = "https://github.com"
                },
                HandlerId = "firefox-personal",
            },
            new MappingRule
            {
                Matcher = new UrlMatcher
                {
                    MatchType = UrlMatchType.AuthorityAndPath,
                    Value = "https://members.medaviebc.ca/"
                },
                HandlerId = "chrome-personal"
            }
        ];
    }
}
