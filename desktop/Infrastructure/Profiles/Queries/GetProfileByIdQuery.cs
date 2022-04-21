using Dapper;
using OrderManager.Domain.Profiles;
using System.Data;

namespace Infrastructure.Profiles.Queries;

public class GetProfileByIdQuery {

    private readonly IDbConnection _connection;

    public GetProfileByIdQuery(IDbConnection connection) {
        _connection = connection;
    }

    public async Task<ReleaseProfile?> GetProfileById(int id) {

        const string query = @"SELECT [Id], [Name] FROM [ReleaseProfiles] WHERE [Id] = @Id;";
        const string pluginQuery = @"SELECT [PluginName] FROM [Profiles_Plugins] WHERE [ProfileId] = @Id;";
        const string labelQuery = @"SELECT [LabelId] FROM [Profiles_Labels] WHERE [ProfileId] = @Id;";
        const string emailQuery = @"SELECT [EmailId] FROM [Profiles_Emails] WHERE [ProfileId] = @Id;";

        var dto = await _connection.QuerySingleAsync<ProfileDto>(query, new {
            Id = id
        });

        var plugins = await _connection.QueryAsync<string>(pluginQuery, new {
            Id = id
        });

        var labels = await _connection.QueryAsync<int>(labelQuery, new {
            Id = id
        });

        var emails = await _connection.QueryAsync<int>(emailQuery, new {
            Id = id
        });

        return new(id, dto.Name ?? "", emails, labels, plugins);

    }

    private class ProfileDto {
        public int Id { get; set; }
        public string? Name { get; set; }
    }

}