namespace OrderManager.Domain.Emails;

public class EmailTemplate {

    public int Id { get; init; }

    public string Name { get; private set; }

    public string TemplatePath { get; private set; }

    public string Subject { get; private set; }

    public string Body { get; private set; }

    private readonly List<string> _to;
    public IReadOnlyCollection<string> To => _to;

    private readonly List<string> _cc;
    public IReadOnlyCollection<string> Cc => _cc;

    private readonly List<string> _bcc;
    public IReadOnlyCollection<string> Bcc => _bcc;

    public EmailTemplate(int id, string name, string templatePath) {
        Id = id;
        Name = name;
        TemplatePath = templatePath;
        Subject = "";
        Body = "";
        _to = new();
        _cc = new();
        _bcc = new();
    }

    public EmailTemplate(int id, string name, string templatePath, string subject, string body, IEnumerable<string> to, IEnumerable<string> cc, IEnumerable<string> bcc) {
        Id = id;
        Name = name;
        TemplatePath = templatePath;
        Subject = subject;
        Body = body;
        _to = new(to);
        _cc = new(cc);
        _bcc = new(bcc);
    }

    public void SetName(string name) {
        Name = name;
    }

    public void SetTemplatePath(string templatePath) {
        TemplatePath = templatePath;
    }

    public void SetBody(string body) {
        Body = body;
    }

    public void SetSubject(string subject) {
        Subject = subject;
    }

    public void AddTo(string to) {
        _to.Add(to);
    }

    public void AddCc(string cc) {
        _cc.Add(cc);
    }

    public void AddBcc(string bcc) {
        _bcc.Add(bcc);
    }
}
