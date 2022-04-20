namespace OrderManager.ApplicationCore.Common;

public interface IFileIO {

    public string ReadAllText(string fileName);

    public bool Exists(string? path);

    public IEnumerable<string> EnumerateDirectories(string path);

}