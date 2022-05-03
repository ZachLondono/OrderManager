using OrderManager.ApplicationCore.Emails;
using System.Net;
using System.Net.Mail;

namespace Infrastructure.Emails;

public class EmailSender : IEmailSender {

    public record EmailServerConfig(string Host, int Port);

    private readonly EmailServerConfig _config;

    public EmailSender(EmailServerConfig config) {
        _config = config;
    }

    public async Task SendEmail(IEmailSender.Email email) {
        
        MailMessage message = new();
        message.From = new(email.Sender);
        
        foreach (var to in email.To) {
            message.To.Add(new(to));
        }
        
        foreach (var cc in email.Cc) {
            message.CC.Add(new(cc));
        }

        foreach (var bcc in email.Bcc) {
            message.Bcc.Add(new(bcc));
        }

        message.IsBodyHtml = true;
        message.Subject = email.Subject;
        message.Body = email.Body;

        SmtpClient client = new();
        client.Host = _config.Host;
        client.Port = _config.Port;
        client.EnableSsl = true;
        client.UseDefaultCredentials = false;
        client.Credentials = new NetworkCredential(email.Sender, email.Password);
        client.DeliveryMethod = SmtpDeliveryMethod.Network;

        await Task.Run(() => client.Send(message));

    }

}
