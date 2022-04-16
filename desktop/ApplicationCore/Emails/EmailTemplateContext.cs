using OrderManager.Domain.Emails;

namespace OrderManager.ApplicationCore.Emails;

public class EmailTemplateContext {

    private EmailTemplate _template;
    private List<object> _events;

    public IEnumerable<object> Events => _events;

    internal EmailTemplateContext(EmailTemplate template) {
        _template = template;
        _events = new();
    }

    public void SetName(string name) => throw new NotImplementedException();

    public void SetTemplatePath(string templatePath) => throw new NotImplementedException();

    public void SetBody(string body) => throw new NotImplementedException();

    public void SetSubject(string subject) => throw new NotImplementedException();

    public void AddTo(string to) => throw new NotImplementedException();

    public void AddCc(string cc) => throw new NotImplementedException();

    public void AddBcc(string bcc) => throw new NotImplementedException();

}
