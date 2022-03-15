using Dapper;
using Microsoft.Extensions.Logging;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManager.Features.Ribbon.ReleaseProfiles;

public class ReleaseProfileRepository {

    private readonly ILogger<ReleaseProfileRepository> _logger;
    private readonly IDbConnection _connection;

    public ReleaseProfileRepository(ILogger<ReleaseProfileRepository> logger, IDbConnection connection) {
        _logger = logger;
        _connection = connection;
    }

    public ReleaseProfileEventDomain GetProfileById(Guid id) {

        const string profileQuery = "SELECT [Name] FROM [ReleaseProfiles] WHERE [Id] = @Id;";
        const string profileActionQuery = "SELECT [Name] FROM [ReleaseProfileActions] WHERE [ProfileId] = @Id;";

        var profile = _connection.QuerySingle<ReleaseProfile>(profileQuery, new { Id = id});
        var actions = _connection.Query<ReleaseProfileAction>(profileActionQuery, new { Id = id });

        profile.Actions = actions.ToList();

        return new ReleaseProfileEventDomain(profile);

    }

    public void Save(ReleaseProfileEventDomain profile) {

        if (_connection.State != ConnectionState.Open)
            _connection.Open();

        var trx = _connection.BeginTransaction();

        CreateProfile(profile.ProfileName, profile.ProfileId, trx);

        var events = profile.GetEvents();

        foreach (var domainEvent in events) {

            if (domainEvent is AddActionEvent addEvent) { 

                AddAction(addEvent.ActionName, addEvent.Position, profile.ProfileId, trx);

            } else if (domainEvent is MoveActionEvent moveEvent) {

                MoveAction(moveEvent.ActionName, moveEvent.Position, profile.ProfileId, trx);

            } else if (domainEvent is RemoveActionEvent removeEvent) {

                RemoveAction(removeEvent.ActionName, profile.ProfileId, trx);

            } else if (domainEvent is ChangeProfileNameEvent changeNameEvent) {

                UpdateName(changeNameEvent.ProfileName, profile.ProfileId, trx);

            }

        }

        profile.ClearEvents();

        trx.Commit();
        _connection.Close();

    }

    private Task CreateProfile(string profileName, Guid profileId, IDbTransaction trx) {

        const string command = @"INSERT OR IGNORE INTO [ReleaseProfiles]
                                ([Id],[Name])
                                VALUES (@Id, @Name);";

        int rows = _connection.Execute(command, new {
            Name = profileName,
            Id = profileId
        }, trx);

        _logger.LogTrace("Create profile command executed, {rows} effected", rows);

        return Task.CompletedTask;

    }

    private Task UpdateName(string profileName, Guid profileId, IDbTransaction trx) {

        const string command = "UPDATE [ReleaseProfiles] SET [Name] = @Name WHERE [Id] = @Id;";

        int rows = _connection.Execute(command, new {
            Name = profileName,
            Id = profileId
        }, trx);

        _logger.LogTrace("Update name command executed, {rows} effected", rows);

        return Task.CompletedTask;

    }

    private Task AddAction(string actionName, int position, Guid profileId, IDbTransaction trx) {

        const string command = @"INSERT INTO [ReleaseProfileActions]
                                ([Name], [Position], [ProfileId])
                                VALUES (@Name, @Position, @ProfileId);";

        int rows = _connection.Execute(command, new {
            Name = actionName,
            Position = position,
            ProfileId = profileId
        }, trx);

        _logger.LogTrace("Add action command executed, {rows} effected", rows);

        return Task.CompletedTask;

    }

    private Task MoveAction(string actionName, int newPosition, Guid profileId, IDbTransaction trx) {

        const string command = @"UPDATE [ReleaseProfileActions] SET [Position] = @Position WHERE [Name] = @Name AND [ProfileId] = @ProfileId;";

        int rows = _connection.Execute(command, new {
                Name = actionName,
                ProfileId = profileId,
                Position = newPosition
        }, trx);

        _logger.LogTrace("Move action command executed, {rows} effected", rows);

        return Task.CompletedTask;

    }

    private Task RemoveAction(string actionName, Guid profileId, IDbTransaction trx) {

        const string command = "DELETE FROM [ReleaseProfileActions] WHERE [Name] = @Name AND [ProfileId] = @ProfileId;";

        int rows = _connection.Execute(command, new {
            Name = actionName,
            ProfileId = profileId
        }, trx);

        _logger.LogTrace("Remove action command executed, {rows} effected", rows);

        return Task.CompletedTask;

    }

}
