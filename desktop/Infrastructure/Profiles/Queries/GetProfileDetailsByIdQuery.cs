using Dapper;
using OrderManager.Domain.Profiles;
using System.Data;

namespace Infrastructure.Profiles.Queries;

public class GetProfileDetailsByIdQuery {

    private readonly IDbConnection _connection;

    public GetProfileDetailsByIdQuery(IDbConnection connection) {
        _connection = connection;
    }

    public async Task<ReleaseProfileDetails> GetProfileDetailsById(int id) {

        const string query = "SELECT [Id], [Name] FROM [ReleaseProfiles] WHERE [Id] = @Id;";
        const string emailQuery = "SELECT [EmailId] FROM [Profiles_Emails] WHERE [ProfileId] = @Id;";
        const string labelQuery = "SELECT [LabelId] FROM [Profiles_Labels] WHERE [ProfileId] = @Id;";
        const string pluginQuery = "SELECT [PluginName] FROM [Profiles_Plugins] WHERE [ProfileId] = @Id;";

        var details = await _connection.QuerySingleAsync<ReleaseProfileDetails>(query, new { Id = id });
        var emailIds = await _connection.QueryAsync<int>(emailQuery, new { Id = id });
        var labelIds = await _connection.QueryAsync<int>(labelQuery, new { Id = id });
        var pluginNames = await _connection.QueryAsync<string>(pluginQuery, new { Id = id });

        details.EmailIds = new(emailIds);
        details.LabelIds = new(labelIds);
        details.PluginNames = new(pluginNames);

        return details;

    }

}