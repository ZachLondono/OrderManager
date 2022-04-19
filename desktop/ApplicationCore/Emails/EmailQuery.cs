using OrderManager.Domain.Emails;

namespace OrderManager.ApplicationCore.Emails;

public class EmailQuery {

    public delegate Task<EmailTemplate?> GetEmailById(int id);

    /// <summary>
    /// Returns a summary of all available email templates
    /// </summary>
    public delegate Task<IEnumerable<EmailTemplateSummary>> GetLabelSummaries();

    /// <summary>
    /// Returns a summary of all email templates in a given profile
    /// </summary>
    public delegate Task<IEnumerable<EmailTemplateSummary>> GetLabelSummariesByProfileId(int id);

    /// <summary>
    /// Returns a detailed email templates with a given id
    /// </summary>
    public delegate Task<EmailTemplateDetails> GetLabelDetailsById(int id);

    /// <summary>
    /// Returns a detailed email templates for all labels in a given profile
    /// </summary>
    public delegate Task<IEnumerable<EmailTemplateDetails>> GetLabelDetailsByProfileId(int id);

}
