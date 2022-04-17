using OrderManager.Domain.Emails;

namespace OrderManager.ApplicationCore.Emails;

public record EmailNameChangedEvent(string Name);
public record EmailBodyChangedEvent(string Body);
public record EmailSubjectChangedEvent(string Subject);
public record EmailToAddedEvent(string To);
public record EmailToRemovedEvent(string To);
public record EmailCcAddedEvent(string Cc);
public record EmailCcRemovedEvent(string Cc);
public record EmailBccAddedEvent(string Bcc);
public record EmailBccRemovedEvent(string Bcc);

public class EmailTemplateContext {

    private readonly EmailTemplate _template;
    private readonly List<object> _events;

    public int Id => _template.Id;
    public IEnumerable<object> Events => _events;

    public EmailTemplateContext(EmailTemplate template) {
        _template = template;
        _events = new();
    }

    public void SetName(string name) {
        _template.SetName(name);
        _events.Add(new EmailNameChangedEvent(name));
    }

    public void SetBody(string body) {
        _template.SetBody(body);
        _events.Add(new EmailBodyChangedEvent(body));
    }

    public void SetSubject(string subject) {
        _template.SetSubject(subject);
        _events.Add(new EmailSubjectChangedEvent(subject));
    }

    public void AddTo(string to) {
        _template.AddTo(to);
        _events.Add(new EmailToAddedEvent(to));
    }

    public void RemoveTo(string to) {
        _template.RemoveTo(to);
        _events.Add(new EmailToRemovedEvent(to));
    }

    public void AddCc(string cc) {
        _template.AddCc(cc);
        _events.Add(new EmailCcAddedEvent(cc));
    }

    public void RemoveCc(string cc) {
        _template.RemoveCc(cc);
        _events.Add(new EmailCcRemovedEvent(cc));
    }

    public void AddBcc(string bcc) {
        _template.AddBcc(bcc);
        _events.Add(new EmailBccAddedEvent(bcc));
    }

    public void RemoveBcc(string bcc) {
        _template.RemoveBcc(bcc);
        _events.Add(new EmailBccRemovedEvent(bcc));
    }

}
