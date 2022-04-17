namespace OrderManager.ApplicationCore.Emails;

public interface IEmailTemplateRepository {

    public Task<EmailTemplateContext> Add(string name);

    public Task Remove(int id);

    public Task<EmailTemplateContext> GetById(int id);

    public Task Save(EmailTemplateContext context);

}
