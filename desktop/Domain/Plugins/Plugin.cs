namespace OrderManager.Domain.Plugins;

public record Plugin {

    public string Name { get; init; }

    public int Version { get; init; }

    public string Path { get; init; }

    public Type StartUpType { get; init; }

    public Plugin(string name, int version, string path, Type startUpType) {
        Name = name;
        Version = version;
        Path = path;
        StartUpType = startUpType;
    }

}
