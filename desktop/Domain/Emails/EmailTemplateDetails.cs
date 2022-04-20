namespace OrderManager.Domain.Emails;

/// <summary>
/// Email template dto with all email data and no domain logic
/// </summary>
public class EmailTemplateDetails {

    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Sender { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public List<string> To { get; set; } = new();

    public List<string> Cc { get; set; } = new();

    public List<string> Bcc { get; set; } = new();

    public string Subject { get; set; } = string.Empty;

    public string Body { get; set; } = string.Empty;

}
