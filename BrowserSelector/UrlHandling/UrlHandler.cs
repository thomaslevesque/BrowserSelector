using BrowserSelector.Browsers;

namespace BrowserSelector.UrlHandling;

public class UrlHandler
{
    public required string Id { get; init; }
    public required string Name { get; set; }
    public required string BrowserId { get; set; }
    public string? ProfileId { get; set; }
    public IList<string> AdditionalArguments { get; set; } = new List<string>();

    public void Open(Uri uri, IBrowserFactory browserFactory)
    {
        var browser = browserFactory.GetBrowser(BrowserId);

        if (!string.IsNullOrEmpty(ProfileId))
        {
            if (browser is IBrowserWithProfiles browserWithProfiles)
            {
                var profile = browserWithProfiles.GetProfiles().FirstOrDefault(p => p.Id == ProfileId);
                if (profile is null)
                {
                    throw new InvalidOperationException($"Profile with id {ProfileId} not found");
                }
                browserWithProfiles.Open(uri, profile, AdditionalArguments);
                return;
            }

            throw new InvalidOperationException($"Browser {BrowserId} does not support profiles");
        }

        browser.Open(uri, AdditionalArguments);
    }
}
