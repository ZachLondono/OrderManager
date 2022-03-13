using Dapper;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManager.Features.Ribbon.ReleaseProfiles;

public class ReleaseProfileRepository {

    private readonly IDbConnection _connection;

    public ReleaseProfileRepository(IDbConnection connection) {
        _connection = connection;
    }

    public ReleaseProfileEventDomain GetProfileById(Guid id) {

        const string profileQuery = "SELECT [Name] FROM [ReleaseProfiles] WHERE [Id] = @Id;";
        const string profileActionQuery = "SELECT [Name] FROM [ReleaseProfileAction] WHERE [ProfileId] = @Id;";

        var profile = _connection.QuerySingle<ReleaseProfile>(profileQuery, new { Id = id});
        var actions = _connection.Query<ReleaseProfileAction>(profileActionQuery, new { Id = id });

        profile.Actions = actions.ToList();

        return new ReleaseProfileEventDomain(profile);

    }

    public void Save(ReleaseProfileEventDomain profile) {

        var trx = _connection.BeginTransaction();
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

        trx.Commit();
        
    }

    private Task UpdateName(string profileName, Guid profileId, IDbTransaction trx) {

        const string command = "UPDATE [ReleaseProfiles] SET [Name] = @Name WHERE [Id] = @Id;";

        _connection.Execute(command, new {
            Name = profileName,
            Id = profileId
        }, trx);

        return Task.CompletedTask;

    }

    private Task AddAction(string actionName, int position, Guid profileId, IDbTransaction trx) {

        const string command = @"INSERT INTO [ReleaseProfileActions]
                                ([Name], [Profile], [ProfileId])
                                VALUES (@Name, @Position, @ProfileId);";

        _connection.Execute(command, new {
            Name = actionName,
            Position = position,
            ProfileId = profileId
        }, trx);

        return Task.CompletedTask;

    }

    private Task MoveAction(string actionName, int newPosition, Guid profileId, IDbTransaction trx) {

        const string command = @"UPDATE [ReleaseProfileActions] SET [Position] = @Position WHERE [Name] = @Name AND [ProfileId] = @ProfileId;";

        _connection.Execute(command, new {
                Name = actionName,
                ProfileId = profileId,
                Position = newPosition
        }, trx);

        return Task.CompletedTask;

    }

    private Task RemoveAction(string actionName, Guid profileId, IDbTransaction trx) {

        const string command = "DELETE FROM [ReleaseProfileAction] WHERE [Name] = @Name AND [ProfileId] = @ProfileId;";

        _connection.Execute(command, new {
            Name = actionName,
            ProfileId = profileId
        }, trx);

        return Task.CompletedTask;

    }

}
