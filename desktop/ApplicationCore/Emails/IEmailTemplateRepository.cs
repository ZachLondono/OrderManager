namespace OrderManager.ApplicationCore.Emails;

public interface IEmailTemplateRepository {

    public Task<EmailTemplateContext> Add(string name, string templatePath) => throw new NotImplementedException();

    public Task Remove(int id) => throw new NotImplementedException();

    public Task<EmailTemplateContext> GetById(int id) => throw new NotImplementedException();

    public Task Save(EmailTemplateContext context) => throw new NotImplementedException();

}
