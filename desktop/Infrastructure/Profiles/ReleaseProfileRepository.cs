using Dapper;
using OrderManager.ApplicationCore.Profiles;
using System.Data;

namespace Infrastructure.Profiles;

public class ReleaseProfileRepository : IReleaseProfileRepository {

    private readonly IDbConnection _connection;
    private readonly ProfileQuery.GetProfileById _query;
    public ReleaseProfileRepository(IDbConnection connection, ProfileQuery.GetProfileById query) {
        _connection = connection;
        _query = query;
    }

    public async Task<ReleaseProfileContext> Add(string name) {
        const string sql = @"INSERT INTO [ReleaseProfiles] ([Name]) VALUES (@Name) RETURNING Id;";
        int newId = await _connection.QuerySingleAsync<int>(sql, new {
            Name = name
        });
        return new(new(newId, name));
    }

    public async Task<ReleaseProfileContext> GetById(int id) {
        var profile = await _query(id);
        if (profile is null) throw new InvalidDataException($"Could not find profile with id '{id}'");
        return new(profile);
    }

    public async Task Remove(int id) {
        const string sql = @"DELETE FROM [Profiles_Plugins] WHERE [ProfileId] = @Id;
                            DELETE FROM [Profiles_Labels] WHERE [ProfileId] = @Id;
                            DELETE FROM [Profiles_Emails] WHERE [ProfileId] = @Id;
                            DELETE FROM [ReleaseProfiles] WHERE [Id] = @Id;";

        await _connection.ExecuteAsync(sql, new {
            Id = id
        });
    }

    public async Task Save(ReleaseProfileContext context) {

        _connection.Open();
        var trx = _connection.BeginTransaction();

        var events = context.Events;
        var id = context.Id;

        foreach (var ev in events) {

            if (ev is ProfileNameChangeEvent nameChangeEvent) {
                await ApplyNameChangeEvent(trx, id, nameChangeEvent);
            } else if (ev is ProfileEmailAddedEvent emailAddedEvent) {
                await ApplyEmailAddedEvent(trx, id, emailAddedEvent);
            } else if (ev is ProfileEmailRemovedEvent emailRemovedEvent) {
                await ApplyEmailRemovedEvent(trx, id, emailRemovedEvent);
            } else if (ev is ProfileLabelAddedEvent labelAddedEvent) {
                await ApplyLabelAddedEvent(trx, id, labelAddedEvent);
            } else if (ev is ProfileLabelRemovedEvent labelRemovedEvent) {
                await ApplyLabelRemovedEvent(trx, id, labelRemovedEvent);
            } else if (ev is ProfilePluginAddedEvent pluginAddedEvent) {
                await ApplyPluginAddedEvent(trx, id, pluginAddedEvent);
            } else if (ev is ProfilePluginRemovedEvent pluginRemovedEvent) {
                await ApplyPluginRemovedEvent(trx, id, pluginRemovedEvent);
            }

        }

        trx.Commit();
        _connection.Close();

    }

    private async Task ApplyNameChangeEvent(IDbTransaction trx, int profileId, ProfileNameChangeEvent ev) {

        const string sql = "UPDATE [ReleaseProfiles] SET [Name] = @Name WHERE [Id] = @Id";

        await _connection.ExecuteAsync(sql, new {
            Id = profileId,
            Name = ev.Name,
        }, trx);

    }

    private async Task ApplyEmailAddedEvent(IDbTransaction trx, int profileId, ProfileEmailAddedEvent ev) {

        const string sql = "INSERT INTO [Profiles_Emails] ([EmailId], [ProfileId]) VALUES (@EmailId, @ProfileId);";

        await _connection.ExecuteAsync(sql, new {
            EmailId = ev.EmailId,
            ProfileId = profileId,
        }, trx);

    }
    
    private async Task ApplyEmailRemovedEvent(IDbTransaction trx, int profileId, ProfileEmailRemovedEvent ev) {

        const string sql = "DELETE FROM [Profiles_Emails] WHERE [EmailId] = @EmailId AND [ProfileId] =  @ProfileId;";

        await _connection.ExecuteAsync(sql, new {
            EmailId = ev.EmailId,
            ProfileId = profileId,
        }, trx);

    }

    private async Task ApplyLabelAddedEvent(IDbTransaction trx, int profileId, ProfileLabelAddedEvent ev) {

        const string sql = "INSERT INTO [Profiles_Labels] ([LabelId], [ProfileId]) VALUES (@LabelId, @ProfileId);";

        await _connection.ExecuteAsync(sql, new {
            LabelId = ev.LabelId,
            ProfileId = profileId,
        }, trx);

    }

    private async Task ApplyLabelRemovedEvent(IDbTransaction trx, int profileId, ProfileLabelRemovedEvent ev) {

        const string sql = "DELETE FROM [Profiles_Labels] WHERE [LabelId] = @LabelId AND [ProfileId] =  @ProfileId;";

        await _connection.ExecuteAsync(sql, new {
            LabelId = ev.LabelId,
            ProfileId = profileId,
        }, trx);

    }

    private async Task ApplyPluginAddedEvent(IDbTransaction trx, int profileId, ProfilePluginAddedEvent ev) {

        const string sql = "INSERT INTO [Profiles_Plugins] ([PluginName], [ProfileId]) VALUES (@PluginName, @ProfileId);";

        await _connection.ExecuteAsync(sql, new {
            PluginName = ev.PluginName,
            ProfileId = profileId,
        }, trx);

    }

    private async Task ApplyPluginRemovedEvent(IDbTransaction trx, int profileId, ProfilePluginRemovedEvent ev) {

        const string sql = "DELETE FROM [Profiles_Plugins] WHERE [PluginName] = @PluginName AND [ProfileId] =  @ProfileId;";

        await _connection.ExecuteAsync(sql, new {
            PluginName = ev.PluginName,
            ProfileId = profileId,
        }, trx);

    }

}