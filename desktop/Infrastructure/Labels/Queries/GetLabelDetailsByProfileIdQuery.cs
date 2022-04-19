using OrderManager.Domain.Labels;
using System.Data;

namespace Infrastructure.Labels.Queries;

public class GetLabelDetailsByProfileIdQuery {

    private readonly IDbConnection _connection;

    public GetLabelDetailsByProfileIdQuery(IDbConnection connection) {
        _connection = connection;
    }

    public Task<IEnumerable<LabelFieldMapDetails>> GetLabelDetailsByProfileId(int id) => throw new NotImplementedException();

}