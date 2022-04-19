using OrderManager.Domain.Emails;
using System.Data;

namespace Infrastructure.Emails.Queries;

public class GetEmailSummariesByProfileIdQuery {

    private readonly IDbConnection _connection;

    public GetEmailSummariesByProfileIdQuery(IDbConnection connection) {
        _connection = connection;
    }

    public Task<IEnumerable<EmailTemplateSummary>> GetEmailSummariesByProfileId(int id) => throw new NotImplementedException();

}