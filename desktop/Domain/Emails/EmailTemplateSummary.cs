namespace OrderManager.Domain.Emails;

/// <summary>
/// Simple email template dto with minimum information and no domain logic
/// </summary>
public class EmailTemplateSummary {

    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

}