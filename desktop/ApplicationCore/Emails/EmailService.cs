using OrderManager.ApplicationCore.Common;
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

    public Task<EmailTemplateContext> CreateEmailTemplate(string templatePath) {
        string? fileName = Path.GetFileNameWithoutExtension(templatePath);

        if (fileName is null) {
            throw new InvalidDataException($"Could not find file from path '{templatePath}'");
        }

        return _repo.Add(fileName, templatePath);
    }

    public async Task SendEmail(Order order, EmailTemplate template, string sender) {

        IEnumerable<string> filledTo = await EvaluateCollectionOfFormulas(order, template.To);
        IEnumerable<string> filledCc = await EvaluateCollectionOfFormulas(order, template.Cc);
        IEnumerable<string> filledBcc = await EvaluateCollectionOfFormulas(order, template.Bcc);

        string filledSubject = await FormulaService.ExecuteFormula(template.Subject, order, "order");

        // TODO: use an html templating engine to create better html emails
        string filledBody = await FormulaService.ExecuteFormula(template.Body, order, "order");

        await _sender.SendEmail(sender, new IEmailSender.Email(filledSubject, filledBody, filledTo, filledCc, filledBcc));

    }

    public static async Task<IEnumerable<string>> EvaluateCollectionOfFormulas(Order order, IEnumerable<string> formulas) {
        List<string> filledValues = new();
        foreach (var formula in formulas)
            filledValues.Add(await FormulaService.ExecuteFormula(formula, order, "order"));

        return filledValues;
    }

}