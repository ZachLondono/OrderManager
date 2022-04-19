using OrderManager.Domain.Profiles;
using System.Data;

namespace Infrastructure.Profiles.Queries;

public class GetProfileDetailsByIdQuery {

    private readonly IDbConnection _connection;

    public GetProfileDetailsByIdQuery(IDbConnection connection) {
        _connection = connection;
    }

    public Task<ReleaseProfileDetails> GetProfileDetailsById(int id) => throw new NotImplementedException();

}