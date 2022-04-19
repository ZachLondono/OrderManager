namespace OrderManager.Domain.Labels;

/// <summary>
/// Simple label field map dto with minimal information and no domain logic
/// </summary>
public class LabelFieldMapSummary {

    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

}
