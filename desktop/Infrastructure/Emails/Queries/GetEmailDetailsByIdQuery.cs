using OrderManager.Domain.Emails;
using System.Data;

namespace Infrastructure.Emails.Queries;

public class GetEmailDetailsByIdQuery {
    
    private readonly IDbConnection _connection;

    public GetEmailDetailsByIdQuery(IDbConnection connection) {
        _connection = connection;
    }

    public Task<EmailTemplateDetails> GetEmailDetailsById(int id) => throw new NotImplementedException();

}
