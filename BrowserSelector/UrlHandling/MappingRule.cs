﻿namespace BrowserSelector.UrlHandling;

public class MappingRule
{
    public required string HandlerId { get; set; }
    public required UrlMatcher Matcher { get; set; }
}
