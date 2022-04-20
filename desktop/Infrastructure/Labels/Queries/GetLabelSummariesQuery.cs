using OrderManager.Domain.Labels;
using System.Data;
using Dapper;

namespace Infrastructure.Labels.Queries;

public class GetLabelSummariesQuery {

    private readonly IDbConnection _connection;

    public GetLabelSummariesQuery(IDbConnection connection) {
        _connection = connection;
    }

    public async Task<IEnumerable<LabelFieldMapSummary>> GetLabelSummaries() {

        const string query = "SELECT [Id], [Name] FROM [LabelFieldMaps];";

        return await _connection.QueryAsync<LabelFieldMapSummary>(query);

    }

}
