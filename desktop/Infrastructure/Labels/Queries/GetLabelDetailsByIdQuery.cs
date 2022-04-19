using OrderManager.Domain.Labels;
using System.Data;

namespace Infrastructure.Labels.Queries;

public class GetLabelDetailsByIdQuery {

    private readonly IDbConnection _connection;

    public GetLabelDetailsByIdQuery(IDbConnection connection) {
        _connection = connection;
    }

    public Task<LabelFieldMapDetails> GetLabelDetailsById(int id) => throw new NotImplementedException();

}
