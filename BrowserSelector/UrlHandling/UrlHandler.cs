namespace BrowserSelector.UrlHandling;

public class UrlHandler
{
    public required string Id { get; init; }
    public required string Name { get; set; }
    public required string BrowserId { get; set; }
    public string? ProfileId { get; set; }
    public IList<string> AdditionalArguments { get; set; } = new List<string>();
}
