namespace BrowserSelector.Model;

public class UrlHandler : IUrlHandler
{
    public required string Id { get; init; }
    public required string BrowserId { get; set; }
    public string? ProfileId { get; set; }
    public IList<string> AdditionalArguments { get; set; } = new List<string>();

    public void Open(string url, IBrowserFactory browserFactory)
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
                browserWithProfiles.Open(url, profile, AdditionalArguments);
            }
            else
            {
                throw new InvalidOperationException($"Browser {BrowserId} does not support profiles");
            }
        }

        browser.Open(url, AdditionalArguments);
    }
}
