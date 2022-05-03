using OrderManager.ApplicationCore.Common;
using OrderManager.Domain.Emails;
using OrderManager.Domain.Orders;
using RazorEngineCore;

namespace OrderManager.ApplicationCore.Emails;

public class EmailService {

    private readonly IEmailSender _sender;
    private readonly IEmailTemplateRepository _repo;

    public EmailService(IEmailSender sender, IEmailTemplateRepository repo) {
        _sender = sender;
        _repo = repo;
    }

    public Task<EmailTemplateContext> CreateEmailTemplate(string templateName) {
        return _repo.Add(templateName);
    }

    public async Task SendEmail(Order order, EmailTemplate template) {

        IEnumerable<string> filledTo = await EvaluateCollectionOfFormulas(order, template.To);
        IEnumerable<string> filledCc = await EvaluateCollectionOfFormulas(order, template.Cc);
        IEnumerable<string> filledBcc = await EvaluateCollectionOfFormulas(order, template.Bcc);

        string filledSubject = await FormulaService.ExecuteFormula(template.Subject, order, "order");

        string filledBody = await FillTemplate(template.Body, order);

        await _sender.SendEmail(new IEmailSender.Email(template.Sender, template.Password, filledSubject, filledBody, filledTo, filledCc, filledBcc));

    }

    public static async Task<IEnumerable<string>> EvaluateCollectionOfFormulas(Order order, IEnumerable<string> formulas) {
        List<string> filledValues = new();
        foreach (var formula in formulas)
            filledValues.Add(await FormulaService.ExecuteFormula(formula, order, "order"));

        return filledValues;
    }

    public static async Task<string> FillTemplate(string templateText, Order order) {
        IRazorEngine razor = new RazorEngine();
        var template = await razor.CompileAsync(templateText);

        return await template.RunAsync(order);
    }

}