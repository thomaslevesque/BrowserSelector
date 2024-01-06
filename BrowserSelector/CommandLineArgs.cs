namespace BrowserSelector;

public record CommandLineArgs(Uri? Uri)
{
    public static CommandLineArgs Parse(string[] args)
    {
        Uri? uri = null;
        if (args.Any())
            Uri.TryCreate(args[0], UriKind.Absolute, out uri);
        return new(uri);
    }
}
