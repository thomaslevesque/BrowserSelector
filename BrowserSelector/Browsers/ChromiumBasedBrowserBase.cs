using System.IO;
using System.Text.Json;

namespace BrowserSelector.Browsers;

public abstract class ChromiumBasedBrowserBase(string id, string name, string executablePath, string userDataPath)
    : BrowserBase(id, name, executablePath), IBrowserWithProfiles
{
    public void Open(Uri uri, BrowserProfile profile, IList<string>? additionalArguments = null)
    {
        additionalArguments =
        [
            ..additionalArguments,
            $"--profile-directory={profile.Id}"
        ];
        base.Open(uri, additionalArguments);
    }

    public IEnumerable<BrowserProfile> GetProfiles()
    {
        var directories = Directory.GetDirectories(userDataPath);
        foreach (var directory in directories)
        {
            var profileId = Path.GetFileName(directory);
            if (string.Equals("System Profile", profileId, StringComparison.OrdinalIgnoreCase))
                continue;

            var preferencesFilePath = Path.Combine(directory, "Preferences");
            if (!File.Exists(preferencesFilePath))
                continue;

            JsonDocument json;
            try
            {
                using var stream = File.OpenRead(preferencesFilePath);
                json = JsonDocument.Parse(stream);
            }
            catch (FileNotFoundException)
            {
                continue;
            }

            var profileElement = json.RootElement.GetProperty("profile");
            var displayName = profileElement.GetProperty("name").GetString();
            yield return new BrowserProfile(profileId, displayName!);
        }
    }
}
