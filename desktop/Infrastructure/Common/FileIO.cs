using OrderManager.ApplicationCore.Common;

namespace Infrastructure.Common;

public class FileIO : IFileIO {

    public IEnumerable<string> EnumerateDirectories(string path) => Directory.EnumerateDirectories(path);

    public bool Exists(string? path)  => File.Exists(path);

    public string ReadAllText(string path) => File.ReadAllText(path);

}