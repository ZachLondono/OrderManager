namespace OrderManager.ApplicationCore.Emails;

public interface IEmailSender {

    public record Email(string Subject, string Body, IEnumerable<string> To, IEnumerable<string> Cc, IEnumerable<string> Bcc);

    public Task SendEmail(string sender, Email email);

}