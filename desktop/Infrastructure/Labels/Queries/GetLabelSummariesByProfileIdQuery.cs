using OrderManager.Domain.Labels;
using System.Data;
using Dapper;

namespace Infrastructure.Labels.Queries;

public class GetLabelSummariesByProfileIdQuery {

    private readonly IDbConnection _connection;

    public GetLabelSummariesByProfileIdQuery(IDbConnection connection) {
        _connection = connection;
    }

    public async Task<IEnumerable<LabelFieldMapSummary>> GetLabelSummariesByProfileId(int profileId) {

        const string query = @"SELECT (LabelFieldMaps.[Id], [Name], [ProfileId])
                                FROM [LabelFieldMaps]
                                RIGHT JOIN [Profiles_Labels] On LabelFieldMaps.Id = Profiles_Labels.LabelId
                                WHERE Profiles_Labels.ProfileId = @ProfileId;";

        return await _connection.QueryAsync<LabelFieldMapSummary>(query, new {
            ProfileId = profileId
        });

    }

}
