namespace BrowserSelector.Browsers;

public interface IBrowser
{
    string Name { get; }
    void Open(Uri uri, IList<string>? additionalArguments = null);
}

public interface IBrowserWithProfiles : IBrowser
{
    void Open(Uri uri, BrowserProfile profile, IList<string>? additionalArguments = null);
    IEnumerable<BrowserProfile> GetProfiles();
}
