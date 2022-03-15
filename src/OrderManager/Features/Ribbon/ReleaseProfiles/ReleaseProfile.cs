using System;
using System.Collections.Generic;
using System.Linq;

namespace OrderManager.Features.Ribbon.ReleaseProfiles;

/// <summary>
/// Represents a set of steps to preform in order to release an order into production
/// </summary>
public class ReleaseProfile {

    public Guid Id { get; init; }

    public string Name { get; set; }

    public List<ReleaseProfileAction> Actions { get; set; }

    public ReleaseProfile(Guid id, string name, List<ReleaseProfileAction> actions) {
        Id = id;
        Name = name;
        Actions = actions;
    }

    public ReleaseProfile(Guid id, string name) : this(id, name, new()) { }

}

public record ReleaseProfileAction(Guid ProfileId, string ActionName);

public class ReleaseProfileEventDomain {

    public string ProfileName => _profile.Name;

    public Guid ProfileId => _profile.Id;

    private readonly ReleaseProfile _profile;
    private readonly List<object> _events;

    public ReleaseProfileEventDomain(ReleaseProfile profile) {
        _profile = profile;
        _events = new();
    }

    public IEnumerable<object> GetEvents() {
        return _events;
    }

    public void ClearEvents() {
        _events.Clear();
    }

    /// <summary>
    /// Move an action into a new position. Shift all actions between the old position and the new position to fill the gap
    /// </summary>
    /// <param name="actionName">Name of the action to move</param>
    /// <param name="position">New position to move the action to</param>
    public void MoveActionToPosition(string actionName, int position) {

        int index = _profile.Actions.IndexOf(new (_profile.Id, actionName));

        if (index == -1 || index == position) return;

        _profile.Actions.RemoveAt(index);
        _profile.Actions.Insert(position, new(_profile.Id, actionName));

        int startIndex = index > position ? position : index;
        int endIndex = index < position ? position : index;

        for (int i = startIndex; i < endIndex + 1; i++) {

            var name = _profile.Actions[i].ActionName;

            _events.Add(new MoveActionEvent(i, name));

        }

    }

    public IEnumerable<string> GetActions() => _profile.Actions.Select(a => a.ActionName).OfType<string>();

    public void AddAction(string actionName) {

        if (_profile.Actions.Count(a => a.ActionName == actionName) != 0) return;

        _events.Add(new AddActionEvent(_profile.Actions.Count, actionName));

        _profile.Actions.Add(new (_profile.Id, actionName));

    }

    /// <summary>
    /// Removes an action from the list of actions, shifts all other actions up to fill the gap
    /// </summary>
    /// <param name="actionName">Name of action to remove</param>
    public void RemoveAction(string actionName) {

        int index = _profile.Actions.IndexOf(new(_profile.Id, actionName));

        if (index == -1) return;

        _profile.Actions.RemoveAt(index);

        _events.Add(new RemoveActionEvent(actionName));

        for (int i = index; i < _profile.Actions.Count; i++) {

            _events.Add(new MoveActionEvent(i, _profile.Actions[i].ActionName));

        }

    }

    public void ChangeProfileName(string profileName) {

        if (_profile.Name == profileName) return;

        _profile.Name = profileName;

        _events.Add(new ChangeProfileNameEvent(profileName));

    }

}

public record AddActionEvent(int Position, string ActionName);

public record MoveActionEvent(int Position, string ActionName);

public record RemoveActionEvent(string ActionName);

public record ChangeProfileNameEvent(string ProfileName);