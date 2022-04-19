using OrderManager.Domain.Profiles;
using System.Data;

namespace Infrastructure.Profiles.Queries;

public class GetProfileSummariesQuery {

    private readonly IDbConnection _connection;

    public GetProfileSummariesQuery(IDbConnection connection) {
        _connection = connection;
    }

    public Task<ReleaseProfileSummary> GetProfileSummaries() => throw new NotImplementedException();

}
