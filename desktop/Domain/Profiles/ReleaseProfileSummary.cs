namespace OrderManager.Domain.Profiles;

/// <summary>
/// Simple release profile dto with minimum information and no domain logic
/// </summary>
public class ReleaseProfileSummary {

    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

}
