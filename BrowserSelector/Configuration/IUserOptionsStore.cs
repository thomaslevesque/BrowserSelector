namespace BrowserSelector.Configuration;

public interface IUserOptionsStore
{
    UserOptions Options { get; }
    void Save();
}
