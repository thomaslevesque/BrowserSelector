using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using BrowserSelector.Extensions;
using NString;

namespace BrowserSelector.UrlHandling;

public class UrlMatcher
{
    public UrlMatchType MatchType { get; set; }
    public required string Value { get; set; }

    public bool IsMatch(Uri uri)
    {
        return MatchType switch
        {
            UrlMatchType.Exact => IsExactMatch(uri, false),
            UrlMatchType.ExactIgnoreCase => IsExactMatch(uri, true),
            UrlMatchType.Authority => IsAuthorityMatch(uri),
            UrlMatchType.AuthorityAndPath => IsAuthorityAndPathMatch(uri),
            UrlMatchType.RegularExpression => IsRegexMatch(uri),
            UrlMatchType.Wildcard => IsWildcardMatch(uri),
            _ => throw new ArgumentOutOfRangeException(nameof(MatchType), "Unknown match type")
        };
    }

    private bool IsExactMatch(Uri uri, bool ignoreCase)
    {
        var comparisonType = ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
        return uri.AbsoluteUri.Equals(Value, comparisonType);
    }

    private bool IsAuthorityMatch(Uri uri)
    {
        var valueAuthority = Value;
        if (Uri.TryCreate(Value, UriKind.Absolute, out var valueUri))
            valueAuthority = valueUri.Authority;

        return uri.Authority.Equals(valueAuthority, StringComparison.OrdinalIgnoreCase);
    }

    private bool IsAuthorityAndPathMatch(Uri uri)
    {
        string valueAuthority;
        string valuePath;

        if (Uri.TryCreate(Value, UriKind.Absolute, out var valueUri))
        {
            valueAuthority = valueUri.Authority;
            valuePath = valueUri.AbsolutePath;
        }
        else
        {
            var slashIndex = Value.IndexOf('/');
            if (slashIndex >= 0)
            {
                valueAuthority = Value.Substring(0, slashIndex);
                valuePath = Value.Substring(slashIndex);
            }
            else
            {
                valueAuthority = Value;
                valuePath = string.Empty;
            }
        }

        return uri.Authority.Equals(valueAuthority, StringComparison.OrdinalIgnoreCase)
               && (uri.AbsolutePath.Equals(valuePath)
                   || uri.AbsolutePath.StartsWith(valuePath.EnsureTrailingSlash(), StringComparison.Ordinal));
    }

    private bool IsRegexMatch(Uri uri)
    {
        return Regex.IsMatch(uri.AbsoluteUri, Value);
    }

    private bool IsWildcardMatch(Uri uri)
    {
        return uri.AbsoluteUri.MatchesWildcard(Value);
    }
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum UrlMatchType
{
    Exact,
    ExactIgnoreCase,
    Authority,
    AuthorityAndPath,
    RegularExpression,
    Wildcard
}
