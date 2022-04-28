namespace OrderManager.Domain.Emails;

public class EmailTemplate {

    public int Id { get; init; }

    public string Name { get; private set; }

    public string Sender { get; private set; }

    public string Password { get; private set; }

    public string Subject { get; private set; }

    public string Body { get; private set; }

    private readonly List<string> _to;
    public IReadOnlyCollection<string> To => _to;

    private readonly List<string> _cc;
    public IReadOnlyCollection<string> Cc => _cc;

    private readonly List<string> _bcc;
    public IReadOnlyCollection<string> Bcc => _bcc;

    public EmailTemplate(int id, string name) {
        Id = id;
        Name = name;
        Sender = "";
        Password = "";
        Subject = "";
        Body = "";
        _to = new();
        _cc = new();
        _bcc = new();
    }

    public EmailTemplate(int id, string name, string sender, string password, string subject, string body, IEnumerable<string> to, IEnumerable<string> cc, IEnumerable<string> bcc) {
        Id = id;
        Name = name;
        Sender = sender;
        Password = password;
        Subject = subject;
        Body = body;
        _to = new(to);
        _cc = new(cc);
        _bcc = new(bcc);
    }

    public void SetName(string name) {
        Name = name;
    }

    public void SetBody(string body) {
        Body = body;
    }

    public void SetSubject(string subject) {
        Subject = subject;
    }

    public void SetSender(string sender) {
        Sender = sender;
    }

    public void SetPassword(string password) {
        Password = password;
    }

    public void AddTo(string to) {
        _to.Add(to);
    }

    public void RemoveTo(string To) {
        _to.Remove(To);
    }

    public void AddCc(string cc) {
        _cc.Add(cc);
    }

    public void RemoveCc(string cc) {
        _cc.Remove(cc);
    }

    public void AddBcc(string bcc) {
        _bcc.Add(bcc);
    }

    public void RemoveBcc(string bcc) {
        _bcc.Remove(bcc);
    }

}
