namespace OrderManager.Domain.Emails;

/// <summary>
/// Email template dto with all email data and no domain logic
/// </summary>
public class EmailTemplateDetails {

    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Sender { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public IEnumerable<string> To { get; set; } = Enumerable.Empty<string>();

    public IEnumerable<string> Cc { get; set; } = Enumerable.Empty<string>();

    public IEnumerable<string> Bcc { get; set; } = Enumerable.Empty<string>();

    public string Subject { get; set; } = string.Empty;

    public string Body { get; set; } = string.Empty;

}
