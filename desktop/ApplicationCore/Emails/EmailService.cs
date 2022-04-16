using OrderManager.Domain.Emails;
using OrderManager.Domain.Orders;

namespace OrderManager.ApplicationCore.Emails;

public class EmailService {

    private readonly IEmailSender _sender;
    private readonly IEmailTemplateRepository _repo;

    public EmailService(IEmailSender sender, IEmailTemplateRepository repo) {
        _sender = sender;
        _repo = repo;
    }

    public void CreateEmailTemplate(string templatePath) => throw new NotImplementedException();

    public void SendEmail(Order order, EmailTemplate template) => throw new NotImplementedException();

}