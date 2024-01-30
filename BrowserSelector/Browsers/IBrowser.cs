using System.Windows.Media;

namespace BrowserSelector.Browsers;

public interface IBrowser
{
    string Id { get; }
    string Name { get; }
    void Open(Uri uri, IList<string>? additionalArguments = null);
    string BaseHandlerId { get; }
    ImageSource? GetIcon();
}

public interface IBrowserWithProfiles : IBrowser
{
    void Open(Uri uri, BrowserProfile profile, IList<string>? additionalArguments = null);
    IEnumerable<BrowserProfile> GetProfiles();
}
