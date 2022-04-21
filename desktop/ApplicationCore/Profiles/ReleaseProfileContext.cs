using OrderManager.Domain.Profiles;

namespace OrderManager.ApplicationCore.Profiles;

public record ProfileNameChangeEvent(string Name);
public record ProfileEmailAddedEvent(int EmailId);
public record ProfileEmailRemovedEvent(int EmailId);
public record ProfileLabelAddedEvent(int LabelId);
public record ProfileLabelRemovedEvent(int LabelId);
public record ProfilePluginAddedEvent(string PluginName);
public record ProfilePluginRemovedEvent(string PluginName);

public class ReleaseProfileContext {

    private readonly ReleaseProfile _profile;
    private readonly List<object> _events;

    public int Id => _profile.Id;
    public IReadOnlyCollection<object> Events => _events;

    public ReleaseProfileContext(ReleaseProfile profile) {
        _profile = profile;
        _events = new List<object>();
    }

    public void ClearEvents() {
        _events.Clear();
    }

    public void SetName(string name) {
        _profile.SetName(name);
        _events.Add(new ProfileNameChangeEvent(name));
    }

    public void AddEmailTemplate(int emailId) {
        _profile.AddEmailTemplate(emailId);
        _events.Add(new ProfileEmailAddedEvent(emailId));
    }

    public void RemoveEmailTemplate(int emailId) {
        _profile.RemoveEmailTemplate(emailId);
        _events.Add(new ProfileEmailRemovedEvent(emailId));
    }

    public void AddLabelFieldMap(int labelId) {
        _profile.AddLabelFieldMap(labelId);
        _events.Add(new ProfileLabelAddedEvent(labelId));
    }

    public void RemoveLabelFieldMap(int labelId) {
        _profile.RemoveLabelFieldMap(labelId);
        _events.Add(new ProfileLabelRemovedEvent(labelId));
    }

    public void AddPlugin(string pluginName) {
        _profile.AddPlugin(pluginName);
        _events.Add(new ProfilePluginAddedEvent(pluginName));
    }

    public void RemovePlugin(string pluginName) {
        _profile.RemovePlugin(pluginName);
        _events.Add(new ProfilePluginRemovedEvent(pluginName));
    }
}