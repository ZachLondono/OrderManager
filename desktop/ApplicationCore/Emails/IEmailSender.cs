namespace OrderManager.ApplicationCore.Emails;

public interface IEmailSender {

    public record Email(string Sender, string Password, string Subject, string Body, IEnumerable<string> To, IEnumerable<string> Cc, IEnumerable<string> Bcc);

    public Task SendEmail(Email email);

}