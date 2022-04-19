using OrderManager.Domain.Emails;
using System.Data;

namespace Infrastructure.Emails.Queries;

public class GetEmailSummariesQuery {

    private readonly IDbConnection _connection;

    public GetEmailSummariesQuery(IDbConnection connection) {
        _connection = connection;
    }

    public Task<IEnumerable<EmailTemplateSummary>> GetEmailSummaries() => throw new NotImplementedException();

}
