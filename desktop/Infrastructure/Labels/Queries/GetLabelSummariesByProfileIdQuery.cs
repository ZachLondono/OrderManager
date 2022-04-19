using OrderManager.Domain.Labels;
using System.Data;

namespace Infrastructure.Labels.Queries;

public class GetLabelSummariesByProfileIdQuery {

    private readonly IDbConnection _connection;

    public GetLabelSummariesByProfileIdQuery(IDbConnection connection) {
        _connection = connection;
    }

    public Task<IEnumerable<LabelFieldMapSummary>> GetLabelSummariesByProfileId(int id) => throw new NotImplementedException();

}
