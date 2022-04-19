namespace OrderManager.Domain.Labels;

/// <summary>
/// Label field map dto with no domain logic
/// </summary>
public class LabelFieldMapDetails {

    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string TemplatePath { get; set; } = string.Empty;

    public int PrintQty { get; set; }

    public LabelType Type { get; set; }

    public Dictionary<string, string> Fields { get; set; } = new();

}
