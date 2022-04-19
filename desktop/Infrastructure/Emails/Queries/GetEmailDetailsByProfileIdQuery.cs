using OrderManager.Domain.Emails;
using System.Data;

namespace Infrastructure.Emails.Queries;

public class GetEmailDetailsByProfileIdQuery {

    private readonly IDbConnection _connection;

    public GetEmailDetailsByProfileIdQuery(IDbConnection connection) {
        _connection = connection;
    }

    public Task<IEnumerable<EmailTemplateDetails>> GetEmailDetailsByProfileId(int id) => throw new NotImplementedException();

}