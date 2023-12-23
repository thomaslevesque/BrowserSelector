namespace BrowserSelector.Model;

public interface IBrowser
{
    string Name { get; }
    void Open(string url, IList<string>? additionalArguments = null);
}

public interface IBrowserWithProfiles : IBrowser
{
    void Open(string url, BrowserProfile profile, IList<string>? additionalArguments = null);
    IEnumerable<BrowserProfile> GetProfiles();
}
