using OrderManager.Domain.Labels;
using System.Data;

namespace Infrastructure.Labels.Queries;

public class GetLabelSummariesQuery {

    private readonly IDbConnection _connection;

    public GetLabelSummariesQuery(IDbConnection connection) {
        _connection = connection;
    }

    public Task<IEnumerable<LabelFieldMapSummary>> GetLabelSummaries() => throw new NotImplementedException();

}
