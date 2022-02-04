namespace OrderManager.ApplicationCore.Domain;

/// <summary>
/// Defines the configuration for an email to be sent
/// </summary>
public class Email {

    public int Id { get; set; }

    public string Name { get; set; }

    public string To { get; set; }
    
    public string Cc { get; set; }

    public string Bcc { get; set; }

    public string Subject { get; set; }

    public string Body { get; set; }

}
