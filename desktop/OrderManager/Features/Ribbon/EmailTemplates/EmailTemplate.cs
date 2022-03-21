using System;
using System.Collections.Generic;
using System.Data;

namespace OrderManager.Features.Ribbon.EmailTemplates;

public record DefinedRecipient(string? Name, string Email);

public record Sender(string? Name, string Email, string Password);

public enum Recipient {
    None,
    Customer,
    Vendor, 
    Supplier
}

public class EmailTemplate {

    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public Recipient MainRecipient { get; set; }

    public List<DefinedRecipient> CC { get; set; } = new();

    public List<DefinedRecipient> BCC { get; set; } = new();

    public string Subject { get; set; } = string.Empty;

    public string Body { get; set; } = string.Empty;

}

public class EmailTemplateEventDomain {

    private readonly EmailTemplate _emailTemplate;
    private readonly List<IDomainEvent> _events = new();

    public EmailTemplateEventDomain(EmailTemplate emailTemplate) {
        _emailTemplate = emailTemplate;
    }

    public IEnumerable<IDomainEvent> GetEvents() => _events;

    public void SetName(string name) { }

    public void SetMainRecipient(Recipient recipient) { }

    public void AddCC(DefinedRecipient recipient) { }

    public void RemoveCC(DefinedRecipient recipient) { }

    public void AddBCC(DefinedRecipient recipient) { }

    public void RemoveBCC(DefinedRecipient recipient) { }

    public void SetSubject(string subject) { }

    public void SetBody(string body) { }

}
