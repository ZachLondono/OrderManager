using OrderManager.Domain.Emails;
using System.Data;
using Dapper;

namespace Infrastructure.Emails.Queries;

public class GetEmailSummariesQuery {

    private readonly IDbConnection _connection;

    public GetEmailSummariesQuery(IDbConnection connection) {
        _connection = connection;
    }

    public async Task<IEnumerable<EmailTemplateSummary>> GetEmailSummaries() {

        const string query = "SELECT ([Id], [Name]) FROM [EmailTemplates]";

        return await _connection.QueryAsync<EmailTemplateSummary>(query);

    }

}
