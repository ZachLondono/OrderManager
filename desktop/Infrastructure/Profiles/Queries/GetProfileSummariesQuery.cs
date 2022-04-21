using OrderManager.Domain.Profiles;
using System.Data;
using Dapper;

namespace Infrastructure.Profiles.Queries;

public class GetProfileSummariesQuery {

    private readonly IDbConnection _connection;

    public GetProfileSummariesQuery(IDbConnection connection) {
        _connection = connection;
    }

    public async Task<IEnumerable<ReleaseProfileSummary>> GetProfileSummaries() {

        const string query = "SELECT [Id], [Name] FROM [ReleaseProfiles];";

        return await _connection.QueryAsync<ReleaseProfileSummary>(query);

    }

}
