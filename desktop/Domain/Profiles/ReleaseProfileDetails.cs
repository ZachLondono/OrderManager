namespace OrderManager.Domain.Profiles;

/// <summary>
/// Release profile dto with no domain logic
/// </summary>
public class ReleaseProfileDetails {

    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public List<int> EmailIds { get; set; } = new();

    public List<int> LabelIds { get; set; } = new();

    public List<string> PluginNames { get; set; } = new();

}
